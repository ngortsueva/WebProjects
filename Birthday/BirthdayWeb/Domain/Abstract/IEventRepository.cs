using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface IEventRepository
    {
        IQueryable<UserEvent> Events { get; }
        void SaveEvent(UserEvent userEvent);
        void DeleteEvent(UserEvent userEvent);
        void DeleteEvent(int id);
    }
}
