using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface ILogRepository
    {
        IQueryable<LogMessage> Messages { get; }
        void SaveMessage(LogMessage message);
        void DeleteMessage(LogMessage message);
        void Clear();
    }
}
