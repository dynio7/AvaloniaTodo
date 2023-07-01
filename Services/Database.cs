using System.Collections.Generic;
using TodoListup.Models;

namespace TodoListup.Services
{
    public class Database
    {
        public IEnumerable<TodoItem> GetItems() => new[]
        {
            new TodoItem { Description = "Wstać" },
        };
    }
}