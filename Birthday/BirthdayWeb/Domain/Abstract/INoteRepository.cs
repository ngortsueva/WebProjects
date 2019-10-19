using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface INoteRepository
    {
        IQueryable<UserNote> Notes { get; }
        void Save(UserNote note);
        void Delete(UserNote note);
        void Delete(int id);
    }
}
