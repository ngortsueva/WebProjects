using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class RequestRepository : IRequestRepository 
    {
        private Birthday db;
        public IQueryable<RequestMessage> Requests { get { return db.Requests; } }

        public RequestRepository(Birthday injectDB)
        {
            db = injectDB;
        }

        public void Create(RequestMessage message)
        {
            if (message == null) return;

            if (message.Id == 0)
            {
                db.Requests.Add(message);
            }
            else
            {
                RequestMessage find_m = db.Requests.FirstOrDefault(m => m.Id == message.Id);                
                find_m.Message = message.Message;
                find_m.Date = message.Date;
            }
            db.SaveChanges();
        }
        public void Delete(RequestMessage message)
        {
            if (message == null) return;
            db.Requests.Remove(message);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            RequestMessage message = db.Requests.FirstOrDefault(m => m.Id == id);
            if (message != null)
            {
                db.Requests.Remove(message);
                db.SaveChanges();
            }
        }

        public void Clear()
        {
            db.ClearRequests();
        }
    }
}
