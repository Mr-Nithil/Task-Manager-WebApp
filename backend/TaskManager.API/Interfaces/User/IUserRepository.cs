using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.User;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface IUserRepository
    {
        Task<UserResponseDto?> GetUserAsync(string id);
        Task<UserResponseDto?> UpdateUserAsync(string id, AppUser user);
        Task<UserResponseDto?> SelfDeleteAsync(string id);
    }
}