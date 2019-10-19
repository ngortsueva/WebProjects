using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface ITaskRepository
    {
        IQueryable<UserTask> Tasks { get; }
        void SaveTask(UserTask task);
        void DeleteTask(UserTask task);
        void DeleteTask(int id);
    }
}
