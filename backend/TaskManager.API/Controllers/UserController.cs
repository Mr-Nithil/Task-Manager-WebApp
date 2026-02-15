using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs.User;
using TaskManager.API.Interfaces;

namespace TaskManager.API.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("me")]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userService.GetUserAsync();

            if(user == null)
                return NotFound();

            return Ok(user);
        }

        [HttpPatch]
        [Route("me")]
        public async Task<IActionResult> Update([FromBody] UpdateUserDto dto)
        {
            var user = await _userService.UpdateUserAsync(dto);

            if(user == null)
                return NotFound();

            return Ok(user);
        }
    }
}