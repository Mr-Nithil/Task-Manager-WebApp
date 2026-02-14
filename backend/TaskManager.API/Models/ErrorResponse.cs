using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Models
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } 
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
        public string Path { get; set; }    
    }
}