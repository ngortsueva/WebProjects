using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class EventRepository : IEventRepository
    {
        private Birthday db;

        public IQueryable<UserEvent> Events { get { return db.Events; } }

        public EventRepository(Birthday injectDb) { db = injectDb; }

        public void SaveEvent(UserEvent userEvent)
        {
            if (userEvent == null) return;

            if (userEvent.Id == 0)
            {
                db.Events.Add(userEvent);
                db.SaveChanges();
            }
            else
            {
                UserEvent find_event = db.Events.FirstOrDefault(t => t.Id == userEvent.Id);
                find_event.Name = userEvent.Name;
                find_event.Description = userEvent.Description;
                find_event.CreateTime = userEvent.CreateTime;
                find_event.ModifyTime = userEvent.ModifyTime;
                find_event.BeginTime = userEvent.BeginTime;
                find_event.EndTime = userEvent.EndTime;
                find_event.Notify = userEvent.Notify;
                find_event.RepeatNotify = userEvent.RepeatNotify;
                find_event.RepeatCount = userEvent.RepeatCount;
                find_event.UserName = userEvent.UserName;
                db.SaveChanges();
            }
        }

        public void DeleteEvent(UserEvent userEvent)
        {
            if (userEvent == null) return;

            db.Events.Remove(userEvent);
            db.SaveChanges();
        }

        public void DeleteEvent(int id)
        {
            UserEvent find_event = db.Events.FirstOrDefault(t => t.Id == id);

            if (find_event == null) return;

            db.Events.Remove(find_event);
            db.SaveChanges();
        }
    }
}
