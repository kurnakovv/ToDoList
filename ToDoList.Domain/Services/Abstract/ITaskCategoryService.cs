using System.Collections.Generic;
using ToDoList.Domain.Models;

namespace ToDoList.Domain.Services.Abstract
{
    public interface ITaskCategoryService
    {
        IEnumerable<TaskCategoryModel> GetCategories();
        TaskCategoryModel AddCategory(TaskCategoryModel category);
        TaskCategoryModel UpdateCategory(TaskCategoryModel category);
        void DeleteCategoryById(string id);
    }
}
