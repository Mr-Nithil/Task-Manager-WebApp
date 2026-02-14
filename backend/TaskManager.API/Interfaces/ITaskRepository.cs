using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.API.Models;

namespace TaskManager.API.Interfaces
{
    public interface ITaskRepository
    {
        Task<List<TaskItem>> GetAllAsync(string userId);
        Task<TaskItem> CreateAsync(TaskItem taskItem);
        Task<TaskItem?> GetByIdAsync(int id, string userId);
        Task<TaskItem?> UpdateAsync(int id, TaskItem taskItem, string userId);
        Task<TaskItem?> DeleteAsync(int id, string userId);
        Task<TaskItem?> ToggleCompleteAsync(int id, string userId);
    }
}