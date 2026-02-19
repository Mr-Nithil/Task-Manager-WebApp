using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;
        public UserService(IUserRepository userRepository, IMapper mapper, ICurrentUserService currentUserService)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
        public async Task<UserResponseDto?> GetUserAsync()
        {
            var id = _currentUserService.UserId;

            var user = await _userRepository.GetUserAsync(id!);

            if(user == null)
                return null;

            return user;
        }

        public async Task<UserResponseDto?> SelfDeleteAsync()
        {
            var id = _currentUserService.UserId;

            var user = await _userRepository.SelfDeleteAsync(id!);

            if(user == null)
                return null;

            return user;
        }

        public async Task<UserResponseDto?> UpdateUserAsync(UpdateUserDto dto)
        {
            var user = _mapper.Map<AppUser>(dto);
            
            var id = _currentUserService.UserId;

            var updatedUser = await _userRepository.UpdateUserAsync(id!, user);

            if(updatedUser == null)
                return null;

            return updatedUser;
        }
    }
}