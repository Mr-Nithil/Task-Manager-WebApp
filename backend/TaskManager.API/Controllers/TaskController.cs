using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.API.Data;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Interfaces;

namespace TaskManager.API.Controllers
{
    [Route("api/tasks")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            var taskDto = await _taskService.GetAllTaskAsync();

            return Ok(taskDto);
        }

        [HttpGet]
        [Authorize]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if(task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(CreateTaskItemDto dto)
        {
            var taskDto = await _taskService.CreateTaskAsync(dto);

            return Ok(taskDto);
        }

        
    }
}