import { useDispatch } from 'react-redux'
import RemoveTodoItemButton from '../RemoveTodoItemButton/RemoveTodoItemButton'
import './TodoItem.css'
import { updateTodoItemStatus } from '../../redux/features/todosSlice'

function TodoItem({todoItem}) {
  const dispatch = useDispatch()

  const handleCheckboxChange = () => {
    dispatch(updateTodoItemStatus(todoItem.id))
  }
  
  return (
    <div className='todo-item'>
      <h2 className='todo-item__title'>{todoItem.title}</h2>
      <div className='todo-item__body'>
        <div className='todo-item__category'>{todoItem.category && todoItem.category.name}</div>
        <input type='checkbox' checked={todoItem.isDone} onChange={handleCheckboxChange}/>
        <div className='todo-item__completed-at'>{todoItem.completedAt}</div>
        <RemoveTodoItemButton todoItemId={todoItem.id}/>
      </div>
    </div>
  )
}
export default TodoItem 