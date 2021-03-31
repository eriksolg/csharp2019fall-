using System;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public DbSet<TodoTask> TodoTasks { get; set; } = default!;
        public DbSet<TodoTaskPriority> TodoTaskPriorities { get; set; } = default!;
        public DbSet<TodoTaskCategory> TodoTaskCategories { get; set; } = default!;

        public AppDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<TodoTask>().HasIndex(t => t.TodoTaskName);
            modelBuilder.Entity<TodoTask>().HasIndex(t => t.IsDone);
        }
    }
}