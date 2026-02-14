using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Models;

namespace TaskManager.API.Mappings
{
    public class TaskMappingProfile : Profile
    {
        public TaskMappingProfile()
        {
            CreateMap<TaskItem, TaskItemResponseDto>();
            CreateMap<CreateTaskItemDto, TaskItem>();
        }
    }
}