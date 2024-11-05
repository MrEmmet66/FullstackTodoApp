import { useDispatch } from 'react-redux'
import './RemoveTodoItemButton.css'
import { deleteTodoItem } from '../../redux/features/todosSlice'

function RemoveTodoItemButton({todoItemId}) {
  const dispatch = useDispatch()

  const removeHandler = () => {
    dispatch(deleteTodoItem(todoItemId))
  }

  return (
    <button className='remove-btn' onClick={removeHandler}>Remove</button>
  )
}
export default RemoveTodoItemButton