using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public TaskService(ITaskRepository taskRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _taskRepository = taskRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<TaskItemResponseDto> CreateTaskAsync(CreateTaskItemDto dto)
        {
            var userId = _currentUserService.UserId;
            var task = _mapper.Map<TaskItem>(dto);

            task.UserId = userId!;

            var createdTask = await _taskRepository.CreateAsync(task);

            return _mapper.Map<TaskItemResponseDto>(createdTask);
        }

        public async Task<List<TaskItemResponseDto>> GetAllTaskAsync()
        {
            var userId = _currentUserService.UserId;

            var tasks = await _taskRepository.GetAllAsync(userId!);

            var taskDto = _mapper.Map<List<TaskItemResponseDto>>(tasks);

            return taskDto;
        }

        public async Task<TaskItemResponseDto?> GetTaskByIdAsync(int id)
        {
            var userId = _currentUserService.UserId;

            var task = await _taskRepository.GetByIdAsync(id, userId!);

            if (task == null)
                return null;

            return _mapper.Map<TaskItemResponseDto>(task);
        }
    }
}