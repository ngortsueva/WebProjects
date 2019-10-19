using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface IRequestRepository
    {
        IQueryable<RequestMessage> Requests { get; }
        void Create(RequestMessage message);
        void Delete(RequestMessage message);
        void Clear();
    }
}
