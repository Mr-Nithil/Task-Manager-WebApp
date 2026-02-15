using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.Admin;

namespace TaskManager.API.Interfaces
{
    public interface IAdminService
    {
        Task<List<AdminUserResponseDto>> GetAllUsersAsync();
        Task<AdminUserResponseDto?> GetUserWithTasksAsync(string userId);
        Task<AdminUserResponseDto?> ToggleUserStatusAsync(string userId);

        Task<List<AdminTaskItemResponseDto>> GetAllTasksAsync(); 
        Task<AdminTaskItemResponseDto?> GetTaskByIdAsync(int id);
        Task<AdminTaskItemResponseDto?> DeleteTaskByIdAsync(int id);
    }
}