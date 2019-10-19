using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;

namespace ConsoleSql
{
    class Program
    {
        public static void AddContinents(string connection)
        {
            Citydb db = new Citydb(connection);
            db.Open();

            Continent c1 = new Continent(1, "North America");
            if (db.continents.Find(c1.ContinentID) == null) db.continents.Add(c1);
            else Console.WriteLine("Object [{0}] is exist in database", c1);

            Continent c2 = new Continent(2, "South America");
            if (db.continents.Find(c2.ContinentID) == null) db.continents.Add(c2);
            else Console.WriteLine("Object [{0}] is exist in database", c2);

            Continent c3 = new Continent(3, "Africa");
            if (db.continents.Find(c3.ContinentID) == null) db.continents.Add(c3);
            else Console.WriteLine("Object [{0}] is exist in database", c3);

            Continent c4 = new Continent(4, "Australia");
            if (db.continents.Find(c4.ContinentID) == null) db.continents.Add(c4);
            else Console.WriteLine("Object [{0}] is exist in database", c4);

            Continent c5 = new Continent(5, "Antarctic");
            if (db.continents.Find(c5.ContinentID) == null) db.continents.Add(c5);
            else Console.WriteLine("Object [{0}] is exist in database", c5);

            Continent c6 = new Continent(6, "Eurasia");
            if (db.continents.Find(c6.ContinentID) == null) db.continents.Add(c6);
            else Console.WriteLine("Object [{0}] is exist in database", c6);

            Console.WriteLine("***Continents:");
            db.continents.Print();

            Console.WriteLine("***Delete from list one continent:");
            if(db.continents.Find(c1.ContinentID) != null)db.continents.Delete(c1);
            db.continents.Print();


            Console.WriteLine("***Update list of continents:");
            c2.Name = "Tru-la-la";
            db.continents.Update(c2);
            db.continents.Print();

            Continent find = db.continents.Find("Eurasia");
            if(find!= null)Console.WriteLine("***Find object: [{0}]", find);

            Console.WriteLine("***Get list of continents:");
            IList<Continent> listContinents = db.continents.GetContinentList();
            foreach(Continent item in listContinents)
            {
                Console.WriteLine(item);
            }

            db.Close();
        }        

        public static void AddCountries(string connection)
        {
            Citydb db = new Citydb(connection);
            db.Open();

            Country c1 = new Country(1, 1, "USA");
            if (db.countries.Find(c1.CountryID) == null) db.countries.Add(c1);

            Country c2 = new Country(2, 2, "Brazilia");
            if (db.countries.Find(c2.CountryID) == null) db.countries.Add(c2);

            Country c3 = new Country(3, 3, "Egypt");
            if (db.countries.Find(c3.CountryID) == null) db.countries.Add(c3);

            Country c4 = new Country(4, 4, "Australia");
            if (db.countries.Find(c4.CountryID) == null) db.countries.Add(c4);

            Country c5 = new Country(5, 6, "Sweden");
            if (db.countries.Find(c5.CountryID) == null) db.countries.Add(c5);

            Console.WriteLine("****Countries list:");
            db.countries.Print();

            Console.WriteLine();
            Console.WriteLine("****Delete func:");
            db.countries.Delete(c1);
            db.countries.Print();

            Console.WriteLine();
            Console.WriteLine("****Update func:");
            Console.WriteLine("****Before update: [{0}]", c2);
            c2.Name = "Argentina";
            db.countries.Update(c2);
            Console.WriteLine("****After update: [{0}]", c2);
            db.countries.Print();

            Country find = db.countries.Find("Sweden");
            if (find != null) Console.WriteLine("Find country: [{0}]", find);

            Console.WriteLine("****Get list of countries:");
            IList<Country> listCountries = db.countries.GetCountryList();
            foreach(Country item in listCountries)
            {
                Console.WriteLine(item);
            }

            db.Close();
        }

        public static void AddCity(string connection)
        {
            Citydb db = new Citydb(connection);
            db.Open();

            City c1 = new City(1, 1, "London");
            if (db.cities.Find(c1.CityID) == null) db.cities.Add(c1);

            City c2 = new City(2, 2, "Paris");
            if (db.cities.Find(c2.CountryID) == null) db.cities.Add(c2);

            City c3 = new City(3, 3, "Stockgolm");
            if (db.cities.Find(c3.CountryID) == null) db.cities.Add(c3);

            City c4 = new City(4, 4, "Sydney");
            if (db.cities.Find(c4.CountryID) == null) db.cities.Add(c4);

            City c5 = new City(5, 6, "Berlin");
            if (db.cities.Find(c5.CountryID) == null) db.cities.Add(c5);

            Console.WriteLine("****Cities list:");
            db.cities.Print();

            Console.WriteLine();
            Console.WriteLine("****Delete func:");
            db.cities.Delete(c1);
            db.cities.Print();

            Console.WriteLine();
            Console.WriteLine("****Update func:");
            Console.WriteLine("****Before update: [{0}]", c2);
            c2.Name = "Rome";
            db.cities.Update(c2);
            Console.WriteLine("****After update: [{0}]", c2);
            db.cities.Print();

            City find = db.cities.Find("Sydney");
            if (find != null) Console.WriteLine("Find city: [{0}]", find);

            Console.WriteLine("****Get list of cities:");
            IList<City> listCities = db.cities.GetCityList();
            foreach (City item in listCities)
            {
                Console.WriteLine(item);
            }

            db.Close();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Console database webGeoInfo");
            string cnStr = ConfigurationManager.AppSettings["cnStr"];

            //AddContinents(cnStr);
            //AddCountries(cnStr);
            //AddCity(cnStr);
            Citydb db = new Citydb(cnStr);
            db.Open();

            Console.WriteLine("***Countries:");
            db.countries.Print();

            Console.WriteLine("--------------------------------");
            Console.WriteLine("***Cities:");
            db.cities.Print();

            db.Close();

            Console.WriteLine("END");
            Console.ReadLine();
        }
    }
}
