using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepository _adminRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;
        public AdminService(IAdminRepository adminRepository, ICurrentUserService currentUserService, IMapper mapper)
        {
            _adminRepository = adminRepository;
            _currentUserService = currentUserService;
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

        public async Task<AdminResponseDto?> GetAdminProfileAsync()
        {
            var id = _currentUserService.UserId;

            var user = await _adminRepository.GetAdminProfileAsync(id);

            if(user == null)
                return null;

            return user;
        }

        public async Task<AdminResponseDto?> UpdateAdminProfileAsync(UpdateAdminDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);

            var id = _currentUserService.UserId;

            var updatedUser = await _adminRepository.UpdateAdminProfileAsync(id!, user);

            if(updatedUser == null)
                return null;

            return updatedUser;
        }

        public async Task<AdminUserResponseDto?> DeleteUserAsync(string userId)
        {
            var id = _currentUserService.UserId;

            if(userId == id)
                return null;
                
            var user = await _adminRepository.DeleteUserAsync(userId);

            if(user == null)
                return null;

            return user;
        }
    }
}