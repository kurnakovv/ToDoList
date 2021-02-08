using System.Collections.Generic;
using ToDoList.Data.Entities;

namespace ToDoList.Data.Repositories.Abstract
{
    public interface ITaskCategoryRepository
    {
        IEnumerable<TaskCategoryEntity> GetCategories();
        TaskCategoryEntity AddCategory(TaskCategoryEntity category);
        TaskCategoryEntity UpdateCategory(TaskCategoryEntity category);
        void DeleteCategoryById(string id);
    }
}
