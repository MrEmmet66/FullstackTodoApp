import { useDispatch, useSelector } from 'react-redux'
import './AddTodoItemForm.css'
import { addTodoItem } from '../../redux/features/todosSlice'

function AddTodoItemForm() {
  const categories = [{id: 1, name: 'Work'}, {id: 2, name: 'Home'}, {id: 3, name: 'Study'}]

  const todoItems = useSelector((state) => state.todos)

  const dispatch = useDispatch()

  const handleSubmit = (event) => {
    event.preventDefault()
    const formData = new FormData(event.target)
    const todoItem = Object.fromEntries(formData)

    if(!todoItem.category) {
      todoItem.category = null
    }
    else {
      todoItem.category = categories.find((category) => category.id === Number(todoItem.category))
    }
    todoItem.completedAt = todoItem.completedAt ? new Date(todoItem.completedAt).toISOString() : null
    const payloadTodoItem = {
      title: todoItem.title

    }
    if(todoItem.completedAt) {
        payloadTodoItem.completedAt = todoItem.completedAt
    }
    if(todoItem.category) {
        payloadTodoItem.categoryId = todoItem.category.id
    }
    console.log(todoItem)
    console.log(payloadTodoItem)
    dispatch({type: 'ADD_TODO_ITEM_REQUEST', payload: payloadTodoItem})
  }

  return (
    <div>
      <form className='todo-form' onSubmit={handleSubmit}>
        <input className='todo-form input' type="text" name="title" placeholder='Enter title of your todo' />
        <input className='todo-form input' type="date" name="completedAt" placeholder='Choose date' />
        <select className='todo-form select' name="category">
          <option value=''>Select Category</option>
          {categories.map((category) => {
            return <option key={category.id} value={category.id}>{category.name}</option>
          })}
        </select>
        <button className='todo-form button' type='submit'>Add</button>
      </form>
    </div>
  )
}
export default AddTodoItemForm