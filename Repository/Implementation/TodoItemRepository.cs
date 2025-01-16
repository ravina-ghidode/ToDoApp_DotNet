using Database;
using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Implementation
{
    public class TodoItemRepository : RepositoryBase<TodoItem>,ITodoItemRepository
    {
        public TodoItemRepository(ApplicationContext context) : base(context)
        {
            
        }

        public async Task<TodoItem> AddTodoItemAsync(TodoItem item)
        {
            item.DateCreated = DateTime.Now;
            await AddAsync(item);
            return item;
        }

        public TodoItem DeleteTodoItem(TodoItem item)
        {
            Remove(item);
            return item;
        }

        public async Task<IEnumerable<TodoItem>> GetAllToDoItemsAsync()
        {
            return await GetAllAsync();
        }

        public async Task<TodoItem> GetTodoByIdAsync(int id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<IEnumerable<TodoItem>> GetPagedDataAsync(int pageNumber, int pageSize)
        {
            return await _context.TodoItems
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }

        public  TodoItem UpdateTodoItem(TodoItem item)
        {
            Update(item);
            return item;
        }
    }
}
