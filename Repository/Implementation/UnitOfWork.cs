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
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationContext _context;

        public ITodoItemRepository TodoItems { get; private set; }

        //public IDeveloperRepository DeveloperRepository { get; private set; }

        public UnitOfWork(ApplicationContext context)
        {
            _context = context;

            //DeveloperRepository = new DeveloperRepository(_context);
            TodoItems = new TodoItemRepository(_context);
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
