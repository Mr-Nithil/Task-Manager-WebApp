using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;
        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<UserResponseDto?> GetUserAsync(string id)
        {
            var user = await _applicationDbContext
                .Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if(user == null)
                return null;

            return new UserResponseDto
            {
                Id = user.Id,
                Username = user.UserName!,
                Email = user.Email!,
                IsActive = user.IsActive,

                TotalTasks = user.Tasks.Count(),
                CompletedTasks = user.Tasks.Count(t => t.IsCompleted == true),
                PendingTasks = user.Tasks.Count(t => t.IsCompleted == false),
            }; 
        }

        public async Task<UserResponseDto?> UpdateUserAsync(string id, AppUser user)
        {
            var existingUser = await _applicationDbContext
                .Users
                .Include(u => u.Tasks)
                .FirstOrDefaultAsync(u => u.Id == id);

            if(existingUser == null)
                return null;

            existingUser.UserName = user.UserName;

            await _applicationDbContext.SaveChangesAsync();

            return new UserResponseDto
            {
                Id = existingUser.Id,
                Username = existingUser.UserName!,
                Email = existingUser.Email!,
                IsActive = existingUser.IsActive,

                TotalTasks = existingUser.Tasks.Count(),
                CompletedTasks = existingUser.Tasks.Count(t => t.IsCompleted == true),
                PendingTasks = existingUser.Tasks.Count(t => t.IsCompleted == false),
            };
        }
    }
}