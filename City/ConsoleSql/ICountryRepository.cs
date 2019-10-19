using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSql
{
    public interface ICountryRepository
    {
        void Open();
        void Close();
        void Add(Country city);
        void Delete(Country city);
        void Update(Country city);
        void Save();
        void Print();
        Country Find(int id);
        Country Find(string name);
        IList<Country> GetCountryList();
    }
}
