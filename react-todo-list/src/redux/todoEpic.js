import {combineEpics, ofType} from "redux-observable";
import {from, map, mergeMap} from "rxjs";
import {fetchGraphQl} from "../utils/apiActions.js";
import {ADD_TODO_ITEM_MUTATION, GET_TODO_ITEMS_QUERY} from "./queries.js";
import {addTodoItemFullfilled, getTodoItemsFullfilled} from "./features/todosSlice.js";

const getTodoItemsEpic = action$ =>
    action$.pipe(
        ofType('GET_TODO_ITEMS_REQUEST'),
        mergeMap(() =>
        from(
            fetchGraphQl({
                query: GET_TODO_ITEMS_QUERY
            }))
            .pipe(map(todoItems => (getTodoItemsFullfilled(todoItems))))))

const addTodoItem = action$ =>
    action$.pipe(
        ofType('ADD_TODO_ITEM_REQUEST'),
        mergeMap(({payload}) =>
        from(
            fetchGraphQl({
                query: ADD_TODO_ITEM_MUTATION,
                variables: {
                    todoItem: payload
                }
            }
        ))
    )).pipe(map(todoItem => (addTodoItemFullfilled(todoItem))))

export default combineEpics(getTodoItemsEpic, addTodoItem)