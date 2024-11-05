import {useDispatch, useSelector} from 'react-redux'
import './App.css'
import TodoItemList from './components/TodoItemList/TodoItemList'
import TodoItem from './components/TodoItem/TodoItem'
import AddTodoItemForm from './components/AddTodoItemForm/AddTodoItemForm'
import {useEffect} from "react";

function App() {
  const sortByDone = (a, b) => {
    return (a.isDone === b.isDone) ? 0 : a.isDone ? 1 : -1;
  }
    const dispatch = useDispatch()

    useEffect(() => {
        dispatch({type: 'GET_TODO_ITEMS_REQUEST'})
    }, [dispatch])

    const todoItems = useSelector((state) => {
        return state.todos?.data?.todoItemQuery?.todoItems || [];
    })

    console.log(todoItems)

  return (
    <div className="app">
      <AddTodoItemForm/>
      <TodoItemList>
        {todoItems.map((todoItem) => {
          return <TodoItem key={todoItem.id} todoItem={todoItem} />
        })}
      </TodoItemList>
    </div>
  )
}

export default App
