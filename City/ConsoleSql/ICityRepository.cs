using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSql
{
    public interface ICityRepository
    {
        void Open();
        void Close();
        void Add(City city);
        void Delete(City city);
        void Update(City city);
        void Save();
        void Print();
        City Find(int id);
        City Find(string name);        
        IList<City> GetCityList();
    }
}
