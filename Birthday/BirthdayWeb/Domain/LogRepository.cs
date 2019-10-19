using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class LogRepository : ILogRepository
    {
        private Birthday db;
        public IQueryable<LogMessage> Messages { get { return db.Logs; } }

        public LogRepository(Birthday injectDB)
        {
            db = injectDB;
        }

        public void SaveMessage(LogMessage message)
        {
            if (message == null) return;

            if (message.Id == 0)
            {
                db.Logs.Add(message);
            }
            else
            {
                LogMessage find_m = db.Logs.FirstOrDefault(m => m.Id == message.Id);
                find_m.Source = message.Source;
                find_m.Status = message.Status;
                find_m.Action = message.Action;
                find_m.Message = message.Message;               
            }
            db.SaveChanges();
        }
        public void DeleteMessage(LogMessage message)
        {
            if (message == null) return;
            db.Logs.Remove(message);
            db.SaveChanges();
        }

        public void DeleteMessage(int id)
        {
            LogMessage message = db.Logs.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                db.Logs.Remove(message);
                db.SaveChanges();
            }
        }

        public void Clear()
        {
            db.ClearLogs();
        }
    }
}
