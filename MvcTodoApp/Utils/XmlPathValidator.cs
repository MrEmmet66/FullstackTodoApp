using System.Text.RegularExpressions;

namespace MvcTodoApp.Utils;

public class XmlPathValidator : IStringValidator
{
    private const string XmlPathPattern = @"^(?!-)[a-zA-Z\d\s_.-]+\.[x][m][l]|(?!-)[a-zA-Z\d\s_.-]+\\+[a-zA-Z\d\s_.-]+\.[x][m][l]$";

    public bool Validate(string input)
    {
        return Regex.IsMatch(input, XmlPathPattern);
    }
}