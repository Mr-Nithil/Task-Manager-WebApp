using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.Task;

namespace TaskManager.API.Interfaces
{
    public interface ITaskService
    {
        Task<List<TaskItemResponseDto>> GetAllTaskAsync();
        Task<TaskItemResponseDto> CreateTaskAsync(CreateTaskItemDto dto);
        Task<TaskItemResponseDto?> GetTaskByIdAsync(int id);
        Task<TaskItemResponseDto?> UpdateTaskAsync(int id, UpdateTaskItemDto dto);
        Task<TaskItemResponseDto?> DeleteTaskAsync(int id);
        Task<TaskItemResponseDto?> ToggleCompleteTaskAsync(int id);
    }
}