namespace SimpleToDoApp.Core.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public bool IsCompleted { get; set; }

        public TodoItem(string title)
        {
            Title = title;
            IsCompleted = false;
        }
    }
} 