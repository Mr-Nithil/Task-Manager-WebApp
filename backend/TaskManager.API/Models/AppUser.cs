using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace TaskManager.API.Models
{
    public class AppUser : IdentityUser
    {
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public bool IsActive { get; set; } = true;
        public ICollection<TaskItem> Tasks { get; set; } = new List<TaskItem>();
        public ICollection<IdentityUserRole<string>> UserRoles { get; set; } 
        = new List<IdentityUserRole<string>>();
    }
}