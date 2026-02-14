using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TaskManager.API.Data;
using TaskManager.API.Interfaces;
using TaskManager.API.Models;

namespace TaskManager.API.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly ApplicationDbContext  _applicationDbContext;
        public TaskRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<TaskItem> CreateAsync(TaskItem taskItem)
        {
            await _applicationDbContext.Tasks.AddAsync(taskItem);
            await _applicationDbContext.SaveChangesAsync();
            return taskItem;
        }

        public async Task<List<TaskItem>> GetAllAsync(string userId)
        {
            var tasks = await _applicationDbContext
                .Tasks
                .Where(t => t.UserId == userId)
                .OrderByDescending(t => t.CreatedAt)
                .ToListAsync();

            return tasks;
        }

        public async Task<TaskItem?> GetByIdAsync(int id, string userId)
        {
            return await _applicationDbContext
                .Tasks
                .FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);
        }
    }
}