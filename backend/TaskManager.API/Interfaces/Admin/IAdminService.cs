using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface IAdminService
    {
        Task<AdminResponseDto?> GetAdminProfileAsync();
        Task<AdminResponseDto?> UpdateAdminProfileAsync(UpdateAdminDto dto);
        Task<AdminResponseDto?> SelfDeleteAsync();

        Task<List<AdminUserResponseDto>> GetAllUsersAsync();
        Task<AdminUserResponseDto?> GetUserWithTasksAsync(string userId);
        Task<AdminUserResponseDto?> ToggleUserStatusAsync(string userId);
        Task<AdminUserResponseDto?> DeleteUserAsync(string userId);

        Task<List<AdminTaskItemResponseDto>> GetAllTasksAsync(); 
        Task<AdminTaskItemResponseDto?> GetTaskByIdAsync(int id);
        Task<AdminTaskItemResponseDto?> DeleteTaskByIdAsync(int id);
    }
}