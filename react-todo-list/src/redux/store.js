import {applyMiddleware, configureStore} from "@reduxjs/toolkit";
import todosSlice from "./features/todosSlice";
import {createEpicMiddleware} from "redux-observable";
import todoEpic from "./todoEpic.js";

const epicMiddleware = createEpicMiddleware();

const store = configureStore({
	reducer: {
		todos: todosSlice,
	},
	middleware: (getDefaultMiddleware) => getDefaultMiddleware().concat(epicMiddleware)
})

epicMiddleware.run(todoEpic)

export default store;