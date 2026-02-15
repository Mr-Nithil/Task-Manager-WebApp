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
    [Authorize(Roles = "User")]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;

        public TaskController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var taskDto = await _taskService.GetAllTaskAsync();

            return Ok(taskDto);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);

            if(task == null)
                return NotFound();

            return Ok(task);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateTaskItemDto dto)
        {
            var taskDto = await _taskService.CreateTaskAsync(dto);

            return CreatedAtAction(nameof(GetById), new { id = taskDto.Id }, taskDto);
        }

        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateTaskItemDto dto)
        {
            var taskDto = await _taskService.UpdateTaskAsync(id ,dto);

            if(taskDto == null)
                return NotFound();

            return Ok(taskDto);
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> Delete([FromRoute] int id)
        {
            var taskDto = await _taskService.DeleteTaskAsync(id);

            if(taskDto == null)
                return NotFound();

            return NoContent();
        }

        [HttpPatch]
        [Route("{id:int}/toggle-complete")]
        public async Task<IActionResult> ToggleComplete([FromRoute] int id)
        {
            var task = await _taskService.ToggleCompleteTaskAsync(id);

            if(task == null)
                return NotFound();

            return Ok(task);
        }
    }
}