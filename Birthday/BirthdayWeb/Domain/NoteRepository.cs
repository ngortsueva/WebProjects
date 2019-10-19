using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain
{
    public class NoteRepository : INoteRepository
    {
        private Birthday db;
        public IQueryable<UserNote> Notes { get { return db.Notes; } }

        public NoteRepository(Birthday injectDB)
        {
            db = injectDB;
        }

        public void Save(UserNote note)
        {
            if (note == null) return;

            if (note.Id == 0)
            {
                db.Notes.Add(note);
            }
            else
            {
                UserNote find_note = db.Notes.FirstOrDefault(n => n.Id == note.Id);
                find_note.Caption = note.Caption;
                find_note.Message = note.Message;
                find_note.Category = note.Category;
            }
            db.SaveChanges();
        }

        public void Delete(UserNote note)
        {
            if (note == null) return;

            db.Notes.Remove(note);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            UserNote note = db.Notes.FirstOrDefault(n => n.Id == id);

            if (note == null) return;

            db.Notes.Remove(note);
            db.SaveChanges();
        }
    }
}
