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
    }
}