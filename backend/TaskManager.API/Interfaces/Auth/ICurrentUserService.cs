using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.API.Interfaces
{
    public interface ICurrentUserService
    {
        string? UserId { get; }
    }
}