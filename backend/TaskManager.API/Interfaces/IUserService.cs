using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.User;

namespace TaskManager.API.Interfaces
{
    public interface IUserService
    {
        Task<UserResponseDto?> GetUserAsync();
        Task<UserResponseDto?> UpdateUserAsync(UpdateUserDto dto);
    }
}