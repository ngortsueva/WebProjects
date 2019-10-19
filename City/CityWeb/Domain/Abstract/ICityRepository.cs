using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Abstract
{
    public interface ICityRepository
    {
        IQueryable<City> Cities { get; }
        void SaveCity(City city);
        void DeleteCity(City city);
        void Print();
    }
}
