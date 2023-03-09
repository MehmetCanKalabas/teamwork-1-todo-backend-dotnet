using teamwork_1_todo_backend1.Models;

namespace teamwork_1_todo_backend1.Dtos
{
    public class UserDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class ToDoDto
    {
        public string Id { get; set; }
        public List<TodoListDto> TodoList { get; set; }
    }
    public class TodoListDto
    {
        public string TodoID { get; set; }
        public string Title { get; set; }
        public bool IsDone { get; set; }
    }
}
