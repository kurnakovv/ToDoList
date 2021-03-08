using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core;
using System.Linq;
using ToDoList.Data.Context;
using ToDoList.Data.Entities;
using ToDoList.Data.Repositories.Abstract;

namespace ToDoList.Data.Repositories
{
    public class TaskCategoryRepository : ITaskCategoryRepository
    {
        private readonly TaskCategoryDbContext _dbContext = 
            new TaskCategoryDbContext();
        
        public TaskCategoryEntity AddCategory(TaskCategoryEntity category)
        {
            _dbContext.TaskCategories.Add(category);
            Save();
            return category;
        }

        public void DeleteCategoryById(string id)
        {
            var category = FindCategoryById(id);
            _dbContext.TaskCategories.Remove(category);
            Save();
        }

        public IEnumerable<TaskCategoryEntity> GetCategories()
        {
            return _dbContext.TaskCategories.Include(c => c.Tasks).ToList();
        }

        public TaskCategoryEntity UpdateCategory(TaskCategoryEntity category)
        {
            var findCategory = FindCategoryById(category.Id);
            _dbContext.Entry(findCategory).CurrentValues.SetValues(category);
            Save();
            return findCategory;
        }

        private TaskCategoryEntity FindCategoryById(string id)
        {
            var category = _dbContext.TaskCategories.Find(id);

            if (category is null)
                throw new ObjectNotFoundException("Category not found.");

            return category;
        }

        private void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
