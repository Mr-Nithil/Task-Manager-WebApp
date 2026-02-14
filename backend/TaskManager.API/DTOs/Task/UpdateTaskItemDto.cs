using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.DTOs.Task
{
    public class UpdateTaskItemDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = null!;
        [StringLength(2000, ErrorMessage = "Description must be less than 2000 characters")]
        public string? Description { get; set; } 
        public bool IsCompleted { get; set; } = false;
        [Required(ErrorMessage = "Priority is required")]
        [EnumDataType(typeof(TaskPriority), ErrorMessage = "Invalid priority value")]
        public TaskPriority Priority { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime? DueDate { get; set; }
    }
}