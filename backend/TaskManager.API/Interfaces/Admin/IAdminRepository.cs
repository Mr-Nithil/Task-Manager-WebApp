using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface IAdminRepository
    {
        Task<AdminResponseDto?> GetAdminProfileAsync(string id);
        Task<AdminResponseDto?> UpdateAdminProfileAsync(string id, AppUser user);

        Task<List<AdminUserResponseDto>> GetAllUsersAsync();
        Task<AdminUserResponseDto?> GetUserWithTasksAsync(string userId);
        Task<AdminUserResponseDto?> ToggleUserStatusAsync(string userId);

        Task<List<TaskItem>> GetAllTasksAsync();
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task<TaskItem?> DeleteTaskByIdAsync(int id);
    }
}