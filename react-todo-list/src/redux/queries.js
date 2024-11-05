
export const GET_TODO_ITEMS_QUERY = `
query {
    todoItemQuery {
        todoItems {
            id
            title
            isDone
            completedAt
            category {
                name
            }
        }
    }
}`

export const ADD_TODO_ITEM_MUTATION = `
mutation($todoItem:TodoItemInputType!) {
  addTodoItem(todoItem:$todoItem) {
    id
    title
    completedAt
    createdAt
    category {
      name
    }
  }
}
`