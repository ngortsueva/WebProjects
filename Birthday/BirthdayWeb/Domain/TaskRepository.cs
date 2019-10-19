using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class TaskRepository : ITaskRepository
    {
        private Birthday db;

        public IQueryable<UserTask> Tasks { get { return db.Tasks; } }

        public TaskRepository(Birthday injectDb) { db = injectDb; }

        public void SaveTask(UserTask task)
        {
            if (task == null) return;

            if( task.Id == 0)
            {
                db.Tasks.Add(task);
                db.SaveChanges();
            }
            else
            {
                UserTask find_task = db.Tasks.FirstOrDefault(t => t.Id == task.Id);
                find_task.Name = task.Name;
                find_task.Description = task.Description;
                find_task.CreateTime = task.CreateTime;
                find_task.ModifyTime = task.ModifyTime;
                find_task.BeginTime = task.BeginTime;
                find_task.EndTime = task.EndTime;                
                find_task.Notify = task.Notify;
                find_task.RepeatNotify = task.RepeatNotify;
                find_task.RelevanceValue = task.RelevanceValue;
                find_task.Color = task.Color;
                find_task.Flag = task.Flag;
                find_task.UserName = task.UserName;
                db.SaveChanges();
            }
        }

        public void DeleteTask(UserTask task)
        {
            if (task == null) return;

            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public void DeleteTask(int id)
        {
            UserTask find_task = db.Tasks.FirstOrDefault(t => t.Id == id);

            if (find_task == null) return;

            db.Tasks.Remove(find_task);
            db.SaveChanges();
        }
    }
}
