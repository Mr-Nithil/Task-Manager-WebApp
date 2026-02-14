using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.DTOs;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenService _tokenService;

        public AuthController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        [HttpPost("user/register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            try
            {
                if(!ModelState.IsValid)
                    return BadRequest(ModelState);

                var userExists = await _userManager.FindByEmailAsync(dto.Email);

                if(userExists != null)
                    return BadRequest("Email already registered");

                var user = new AppUser
                {
                    UserName = dto.Username,
                    Email = dto.Email,
                };

                var result = await _userManager.CreateAsync(user, dto.Password);

                if (result.Succeeded)
                {
                    var roleResult = await _userManager.AddToRoleAsync(user, "User");

                    if (!roleResult.Succeeded)
                        return StatusCode(500, roleResult.Errors);

                    var token = await _tokenService.CreateToken(user);

                    return Ok(new AuthResponseDto
                    {
                        Token = token,
                        Username = user.UserName,
                        Email = user.Email
                    });
                }
                else
                {
                    return StatusCode(500, result.Errors);
                }
            }
            catch (Exception e)
            {
                return StatusCode(500, e);
            }
        }

        [HttpPost("user/login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null)
                return Unauthorized("Invalid credentials");
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Invalid credentials");

            var token = await _tokenService.CreateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = user.UserName!,
                Email = user.Email!
            });
        }

        [HttpPost("admin/login")]
        public async Task<IActionResult> AdminLogin([FromBody] LoginDto dto)
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(dto.Email);

            if(user == null)
                return Unauthorized("Invalid credentials");
            
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);

            if(!result.Succeeded)
                return Unauthorized("Invalid credentials");

            if(!await _userManager.IsInRoleAsync(user, "Admin"))
                return Forbid();

            var token = await _tokenService.CreateToken(user);

            return Ok(new AuthResponseDto
            {
                Token = token,
                Username = user.UserName!,
                Email = user.Email!
            });
        }
    }
}