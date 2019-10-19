using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace ConsoleSql
{
    public class City
    {
        public int CityID { get; set; }
        public int CountryID { get; set; }
        public string Name { get; set; }

        public City() { }
        public City(int id, int id_country, string name)
        {
            CityID = id;
            CountryID = id_country;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", CityID, CountryID, Name);
        }
    }
}
