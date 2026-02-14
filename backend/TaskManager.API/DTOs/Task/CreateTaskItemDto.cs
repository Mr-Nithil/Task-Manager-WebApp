using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.DTOs.Task
{
    public class CreateTaskItemDto
    {
        [Required]
        public string Title { get; set; } = null!;
        public string? Description { get; set; } 
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
    }
}