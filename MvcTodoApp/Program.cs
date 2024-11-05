using System.Reflection;
using AutoMapper;
using GraphQL;
using MvcTodoApp.GraphQL;
using MvcTodoApp.GraphQL.Types.InputTypes;
using MvcTodoApp.Models;
using MvcTodoApp.Models.DataTransferObjects;
using MvcTodoApp.Models.Entities;
using MvcTodoApp.Models.Repositories;
using MvcTodoApp.Utils;

namespace MvcTodoApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			string connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
			string todoItemsXml = builder.Configuration.GetConnectionString("TodoItemsXml");
			string todoCategoriesXml = builder.Configuration.GetConnectionString("TodoCategoriesXml");

			XmlPathValidator xmlPathValidator = new XmlPathValidator();
			DbConnectionStringValidator dbConnectionStringValidator = new DbConnectionStringValidator();
			if (!dbConnectionStringValidator.Validate(connectionString) || !xmlPathValidator.Validate(todoItemsXml) || !xmlPathValidator.Validate(todoCategoriesXml))
			{
				throw new ArgumentException("Invalid connection string");
			}

			builder.Services.AddSession();
			builder.Services.AddHttpContextAccessor();

			builder.Services.AddCors(options =>
			{
				options.AddDefaultPolicy(builder =>
				{
					builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
				});
			});
			
			builder.Services.AddTransient<SqlTodoCategoryRepository>(provider =>
				new SqlTodoCategoryRepository(connectionString));
			builder.Services.AddTransient<SqlTodoItemRepository>(provider =>
				new SqlTodoItemRepository(connectionString, provider.GetRequiredService<IMapper>()));
			builder.Services.AddTransient<XmlTodoItemRepository>(provider => 
				new XmlTodoItemRepository(todoItemsXml, provider.GetRequiredService<IMapper>(), todoCategoriesXml));
			builder.Services.AddTransient<XmlTodoCategoryRepository>(provider =>
				new XmlTodoCategoryRepository(todoCategoriesXml));
			
			builder.Services.AddTransient<IRepositoryFactoryProvider, RepositoryFactoryProvider>();
			builder.Services.AddAutoMapper(cfg =>
			{
				cfg.AllowNullDestinationValues = true;
				cfg.CreateMap<TodoItemDto, TodoItem>()
					.ReverseMap().AfterMap((src, dest) =>
					{
						dest.CategoryId = src.Category == null || src.Category?.Id == 0 ? null : src.Category?.Id; 
					});
				cfg.CreateMap<TodoItem, TodoItemInputType>()
					.ReverseMap();
			}, Assembly.GetEntryAssembly());
			
			ValueConverter.Register<TodoItemDto, TodoItemInputType>(dto =>
			{
				return new TodoItemInputType(dto);
			});
			
			builder.Services.AddGraphQL(builder =>
			{
				builder.AddSystemTextJson();
				builder.AddSchema<TodoItemSchema>();
				builder.AddGraphTypes();
			});
			


			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseCors();
			app.UseSession();
			app.UseHttpsRedirection();
			app.UseStaticFiles();
			
			app.UseGraphQL();
			app.UseGraphQLPlayground();
			
			app.UseRouting();
			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}