using System.Data.Entity;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Context
{
    internal class TaskCategoryDbContext : DbContext
    {
        public TaskCategoryDbContext() : base("ToDoList") { }
        public DbSet<TaskCategoryEntity> TaskCategories { get; set; }
    }
}
