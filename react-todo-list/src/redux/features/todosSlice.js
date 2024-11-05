import {createSlice, current} from "@reduxjs/toolkit";

export const todosSlice = createSlice({
	name: "todos",
	initialState: {
		todoItems: [],
		todoCategories: []
	},
	reducers: {
		getTodoItemsFullfilled(state, action) {
			return action.payload;
		},
		addTodoItemFullfilled(state, action) {

			state.data.todoItemQuery.todoItems.push(action.payload.data.addTodoItem)
		},
		updateTodoItemStatus(state, action) {
			const todo = state.find(todo => todo.id === action.payload);
			if (todo) {
				todo.isDone = !todo.isDone;
			}
		},
		deleteTodoItem(state, action) {
			return state.filter(todo => todo.id !== action.payload);
		},
	}
})

export const { getTodoItemsFullfilled, addTodoItemFullfilled, addTodoItem, updateTodoItemStatus, deleteTodoItem } = todosSlice.actions;
export default todosSlice.reducer;