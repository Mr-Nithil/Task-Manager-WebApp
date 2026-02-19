using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs.Admin;
using TaskManager.API.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/admin")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;
        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpGet]
        [Route("tasks")]
        public async Task<IActionResult> GetAllTasks()
        {
            var taskDto = await _adminService.GetAllTasksAsync();

            return Ok(taskDto);
        }

        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            var userDto = await _adminService.GetAllUsersAsync();

            return Ok(userDto);
        }

        [HttpGet]
        [Route("tasks/{id:int}")]
        public async Task<IActionResult> GetTaskById([FromRoute] int id)
        {
            var taskDto = await _adminService.GetTaskByIdAsync(id);

            if(taskDto == null)
                return NotFound();

            return Ok(taskDto);
        }

        [HttpGet]
        [Route("users/{userId}")]
        public async Task<IActionResult> GetUserWithTasks([FromRoute] string userId)
        {
            var userDto = await _adminService.GetUserWithTasksAsync(userId);

            if(userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpDelete]
        [Route("tasks/{id:int}")]
        public async Task<IActionResult> DeleteTask([FromRoute] int id)
        {
            var taskDto = await _adminService.DeleteTaskByIdAsync(id);

            if(taskDto == null)
                return NotFound();

            return NoContent();
        }

        [HttpPatch]
        [Route("users/{userId}/toggle-status")]
        public async Task<IActionResult> ToggleUserStatus([FromRoute] string userId)
        {
            var userDto = await _adminService.ToggleUserStatusAsync(userId);

            if(userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetAdminProfile()
        {
            var userDto = await _adminService.GetAdminProfileAsync();

            if(userDto == null)
                return NotFound();

            return Ok(userDto);
        }

        [HttpPatch]
        [Route("me")]
        public async Task<IActionResult> UpdateAdminProfile([FromBody] UpdateAdminDto dto)
        {
            var userDto = await _adminService.UpdateAdminProfileAsync(dto);

            if(userDto == null)
                return NotFound();

            return Ok(userDto);
        }
    }
}