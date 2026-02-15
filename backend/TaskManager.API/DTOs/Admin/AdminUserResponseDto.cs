using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.DTOs.Task;
using TaskManager.API.Models;

namespace TaskManager.API.DTOs.Admin
{
    public class AdminUserResponseDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public int PendingTasks { get; set; }
        public ICollection<TaskItemResponseDto> Tasks { get; set; } = new List<TaskItemResponseDto>();
    }
}