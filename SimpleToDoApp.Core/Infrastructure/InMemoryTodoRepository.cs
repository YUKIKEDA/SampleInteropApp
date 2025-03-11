using SimpleToDoApp.Core.Interfaces;
using SimpleToDoApp.Core.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SimpleToDoApp.Core.Infrastructure
{
    public class InMemoryTodoRepository : ITodoRepository
    {
        private readonly List<TodoItem> _todos;
        private int _currentId;

        public InMemoryTodoRepository()
        {
            _todos = new List<TodoItem>();
            _currentId = 1;
        }

        public Task<IEnumerable<TodoItem>> GetAllAsync()
        {
            return Task.FromResult(_todos.AsEnumerable());
        }

        public Task<TodoItem> AddAsync(TodoItem todoItem)
        {
            todoItem.Id = _currentId++;
            _todos.Add(todoItem);
            return Task.FromResult(todoItem);
        }

        public Task DeleteAsync(int id)
        {
            var todoItem = _todos.FirstOrDefault(t => t.Id == id);
            if (todoItem != null)
            {
                _todos.Remove(todoItem);
            }
            return Task.CompletedTask;
        }
    }
} 