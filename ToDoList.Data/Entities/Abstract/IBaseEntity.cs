using System;

namespace ToDoList.Data.Entities.Abstract
{
    interface IBaseEntity
    {
        string Id { get; set; }
        DateTime DateTime { get; set; }
    }
}
