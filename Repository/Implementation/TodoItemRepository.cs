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

        public async Task<IEnumerable<TodoItem>> GetPagedDataAsync(int pageNumber, int pageSize)
        {
            return await _context.TodoItems
                             .Skip((pageNumber - 1) * pageSize)
                             .Take(pageSize)
                             .ToListAsync();
        }
    }
}
