import './TodoItemList.css'

function TodoItemList({children}) {
  return (
    <div className="todo-list">
      {children}
    </div>
  )
}
export default TodoItemList