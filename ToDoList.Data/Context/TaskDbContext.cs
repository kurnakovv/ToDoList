using System.Data.Entity;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Context
{
    public class TaskDbContext : DbContext
    {
        public TaskDbContext() : base("ToDoList") { }
        public DbSet<TaskEntity> Tasks { get; set; }
    }
}
