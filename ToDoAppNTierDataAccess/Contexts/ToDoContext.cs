using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ToDoAppNTierDataAccess.Configurations;
using ToDoAppNTierEntities.Domains;

namespace ToDoAppNTierDataAccess.Contexts
{
    public class ToDoContext:DbContext
    {
        public ToDoContext(DbContextOptions<ToDoContext> options) : base(options)
        {

        }
        public DbSet<ToDo> ToDos { get; set; }
        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ToDoConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            base.OnModelCreating(modelBuilder);
        }
    }
}
