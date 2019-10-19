using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSql
{
    public interface IContinentRepository
    {
        void Open();
        void Close();
        void Add(Continent city);
        void Delete(Continent city);
        void Update(Continent city);
        void Save();
        void Print();
        Continent Find(int id);
        Continent Find(string name);
        IList<Continent> GetContinentList();
    }
}
