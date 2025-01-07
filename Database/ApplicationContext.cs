using Entities.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext(DbContextOptions options) : base(options)
        {
                
        }
        public DbSet<TodoItem> TodoItems { get; set; }
        public DbSet<Developer> Developers { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TodoItem>().HasData(
                new TodoItem { Id = 1, Title = "Sleeping", IsCompleted = false, DateCreated = DateTime.Now },
                new TodoItem { Id = 2, Title = "Cooking", IsCompleted = false, DateCreated = DateTime.Now },
                new TodoItem { Id = 3, Title = "Playing", IsCompleted = false, DateCreated = DateTime.Now },
                new TodoItem { Id = 4, Title = "Playing", IsCompleted = false, DateCreated = DateTime.Now }
                );
        }
    }
}
