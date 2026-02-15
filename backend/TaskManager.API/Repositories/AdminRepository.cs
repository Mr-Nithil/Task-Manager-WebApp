using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public AdminRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<TaskItem?> DeleteTaskByIdAsync(int id)
        {
            var task = await _applicationDbContext
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == id);

            if(task == null)
                return null;

            _applicationDbContext.Remove(task);
            await _applicationDbContext.SaveChangesAsync();

            return task;
        }

        public async Task<AdminUserResponseDto?> ToggleUserStatusAsync(string userId)
        {
            var user = await _applicationDbContext
                .Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null)
                return null;

            user.IsActive = !user.IsActive;
            await _applicationDbContext.SaveChangesAsync();

            return new AdminUserResponseDto
                {
                    Id = user.Id,
                    Username = user.UserName!,
                    Email = user.Email!,
                    CreatedAt = user.CreatedAt,
                    IsActive = user.IsActive,

                    TotalTasks = user.Tasks.Count(),
                    CompletedTasks = user.Tasks.Count(t => t.IsCompleted == true),
                    PendingTasks = user.Tasks.Count(t => t.IsCompleted == false),

                    Tasks = user.Tasks.Select(t => new TaskItemResponseDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        IsCompleted = t.IsCompleted,
                        Priority = t.Priority,
                        DueDate = t.DueDate,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt,
                    }).ToList()
                };
        }

        public async Task<List<TaskItem>> GetAllTasksAsync()
        {
            var tasks = await _applicationDbContext
                .Tasks
                .OrderByDescending(t => t.CreatedAt)
                .Include(t => t.User)
                .ToListAsync();

            return tasks;
        }

        public async Task<List<AdminUserResponseDto>> GetAllUsersAsync()
        {
            var users = await _applicationDbContext
                .Users
                .Select(u => new AdminUserResponseDto
                {
                    Id = u.Id,
                    Username = u.UserName!,
                    Email = u.Email!,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive,

                    TotalTasks = u.Tasks.Count(),
                    CompletedTasks = u.Tasks.Count(t => t.IsCompleted == true),
                    PendingTasks = u.Tasks.Count(t => t.IsCompleted == false),

                    Tasks = u.Tasks.Select(t => new TaskItemResponseDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        IsCompleted = t.IsCompleted,
                        Priority = t.Priority,
                        DueDate = t.DueDate,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt,
                    }).ToList()
                })
                .OrderByDescending(u => u.CreatedAt)
                .ToListAsync();

            return users;
        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            var task = await _applicationDbContext
                .Tasks
                .Include(t => t.User)
                .FirstOrDefaultAsync(t => t.Id == id);

            if(task == null)
                return null;

            return task;
        }

        public async Task<AdminUserResponseDto?> GetUserWithTasksAsync(string userId)
        {
            var user = await _applicationDbContext
                .Users
                .Select(u => new AdminUserResponseDto
                {
                    Id = u.Id,
                    Username = u.UserName!,
                    Email = u.Email!,
                    CreatedAt = u.CreatedAt,
                    IsActive = u.IsActive,

                    TotalTasks = u.Tasks.Count(),
                    CompletedTasks = u.Tasks.Count(t => t.IsCompleted == true),
                    PendingTasks = u.Tasks.Count(t => t.IsCompleted == false),

                    Tasks = u.Tasks.Select(t => new TaskItemResponseDto
                    {
                        Id = t.Id,
                        Title = t.Title,
                        Description = t.Description,
                        IsCompleted = t.IsCompleted,
                        Priority = t.Priority,
                        DueDate = t.DueDate,
                        CreatedAt = t.CreatedAt,
                        UpdatedAt = t.UpdatedAt
                    }).ToList()
                })
                .FirstOrDefaultAsync(u => u.Id == userId);

            if(user == null)
                return null;

            return user;
        }
    }
}