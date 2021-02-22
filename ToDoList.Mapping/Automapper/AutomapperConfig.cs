using AutoMapper;
using ToDoList.Data.Entities;
using ToDoList.Domain.Models;

namespace ToDoList.Mapping.Automapper
{
    public class AutomapperConfig
    {
        public static IMapper MapConfig()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TaskEntity, TaskModel>()
                    .ReverseMap();

                cfg.CreateMap<TaskCategoryEntity, TaskCategoryModel>()
                    .ReverseMap();
            });

            IMapper mapper = new Mapper(config);

            return mapper;
        }
    }
}
