using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityWeb.Domain.Entities;

namespace CityWeb.Domain.Abstract
{
    public interface IContinentRepository
    {
        IQueryable<Continent> Continents { get; }
        void SaveContinent(Continent continent);
        void DeleteContinent(Continent continent);
        void Print();
    }
}
