using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.DTOs.Task
{
    public class TaskItemResponseDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Description { get; set; } 
        public bool IsCompleted { get; set; } = false;
        public TaskPriority Priority { get; set; }
        public DateTime? DueDate { get; set; }
        //public string UserId { get; set; }
    }
}