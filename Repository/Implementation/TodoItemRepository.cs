using Database;
using Entities.Models;
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
        
    }
}
