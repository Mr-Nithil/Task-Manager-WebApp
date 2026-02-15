using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.Admin;
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
            CreateMap<UpdateTaskItemDto, TaskItem>();

            CreateMap<TaskItem, AdminTaskItemResponseDto>()
                .ForMember(dest => dest.Username, opt => opt.MapFrom(src => src.User.UserName))
                .ForMember(dest => dest.UserEmail, opt => opt.MapFrom(src => src.User.Email));
            CreateMap<AppUser, AdminUserResponseDto>();
        }
    }
}