using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;

namespace ConsoleSql
{
    public class Citydb 
    {
        private string strConnection;

        public CityRepository cities;
        public CountryRepository countries;
        public ContinentRepository continents;

        public Citydb(string connection)
        {
            strConnection = connection;
            cities = new CityRepository(connection);
            countries = new CountryRepository(connection);
            continents = new ContinentRepository(connection);
        }

        public void Open()
        {
            cities.Open();
            countries.Open();
            continents.Open();
        }

        public void Close()
        {
            cities.Close();
            countries.Close();
            continents.Close();
        }
    }
}
