using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.Interfaces;

namespace TaskManager.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly IMapper _mapper;
        public AdminService(IAdminRepository adminRepository, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _mapper = mapper;
        }

        public async Task<AdminTaskItemResponseDto?> DeleteTaskByIdAsync(int id)
        {
            var task = await _adminRepository.DeleteTaskByIdAsync(id);

            if(task == null)
                return null;

            return _mapper.Map<AdminTaskItemResponseDto>(task);
        }

        public async Task<AdminUserResponseDto?> ToggleUserStatusAsync(string userId)
        {
            var user = await _adminRepository.ToggleUserStatusAsync(userId);

            if(user == null)
                return null;

            return user;
        }

        public async Task<List<AdminTaskItemResponseDto>> GetAllTasksAsync()
        {
            var tasks = await _adminRepository.GetAllTasksAsync();

            return _mapper.Map<List<AdminTaskItemResponseDto>>(tasks);
        }

        public async Task<List<AdminUserResponseDto>> GetAllUsersAsync()
        {
            var users = await _adminRepository.GetAllUsersAsync();

            return users;
        }

        public async Task<AdminTaskItemResponseDto?> GetTaskByIdAsync(int id)
        {
            var task = await _adminRepository.GetTaskByIdAsync(id);

            if(task == null)
                return null;

            return _mapper.Map<AdminTaskItemResponseDto>(task);
        }

        public async Task<AdminUserResponseDto?> GetUserWithTasksAsync(string userId)
        {
            var user = await _adminRepository.GetUserWithTasksAsync(userId);

            if(user == null)
                return null;

            return user;
        }
    }
}