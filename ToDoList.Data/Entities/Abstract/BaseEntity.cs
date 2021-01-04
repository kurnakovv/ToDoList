using System;

namespace ToDoList.Data.Entities.Abstract
{
    public abstract class BaseEntity : IBaseEntity
    {
        public string Id { get; set; }
        public DateTime DateTime { get; set; }

        public BaseEntity()
        {
            Id = Guid.NewGuid().ToString();
            DateTime = DateTime.Now;
        }
    }
}
