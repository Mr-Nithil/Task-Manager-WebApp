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

        public async Task<TaskItem?> DeleteAsync(int id, string userId)
        {
            var task = await _applicationDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if(task == null)
                return null;
            
            _applicationDbContext.Tasks.Remove(task);
            await _applicationDbContext.SaveChangesAsync();

            return task;
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

        public async Task<TaskItem?> ToggleCompleteAsync(int id, string userId)
        {
            var task = await _applicationDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if(task == null)
                return null;

            task.IsCompleted = !task.IsCompleted;

            await _applicationDbContext.SaveChangesAsync();

            return task;
        }

        public async Task<TaskItem?> UpdateAsync(int id, TaskItem taskItem, string userId)
        {
            var existingTask = await _applicationDbContext.Tasks.FirstOrDefaultAsync(t => t.Id == id && t.UserId == userId);

            if(existingTask == null)
                return null;

            existingTask.Title = taskItem.Title;
            existingTask.Description = taskItem.Description;
            existingTask.IsCompleted = taskItem.IsCompleted;
            existingTask.Priority = taskItem.Priority;
            existingTask.DueDate = taskItem.DueDate;

            await _applicationDbContext.SaveChangesAsync();

            return existingTask;
        }
    }
}