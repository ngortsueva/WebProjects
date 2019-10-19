using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleAppEFCore
{
    class Program
    {
        public static void AddContinents()
        {
            ContinentRepository cr = new ContinentRepository();
            cr.SaveContinent(new Continent() { name = "North America" });
            cr.SaveContinent(new Continent() { name = "South America" });
            cr.SaveContinent(new Continent() { name = "Africa" });
            cr.SaveContinent(new Continent() { name = "Antarctica" });
            cr.SaveContinent(new Continent() { name = "Australia" });
            cr.SaveContinent(new Continent() { name = "Eurasia" });
        }

        public static void AddCountries()
        {
            CountryRepositories cr = new CountryRepositories();
            cr.SaveCountry(new Country() { id_continent = 2, name = "USA" });
            cr.SaveCountry(new Country() { id_continent = 2, name = "Canada" });
            cr.SaveCountry(new Country() { id_continent = 2, name = "Mexica" });
            cr.SaveCountry(new Country() { id_continent = 3, name = "Argentina" });
            cr.SaveCountry(new Country() { id_continent = 3, name = "Brazilia" });
            cr.SaveCountry(new Country() { id_continent = 4, name = "Egypt" });
            cr.SaveCountry(new Country() { id_continent = 6, name = "Australia" });
            cr.SaveCountry(new Country() { id_continent = 7, name = "Germany" });
            cr.SaveCountry(new Country() { id_continent = 7, name = "Italia" });
            cr.SaveCountry(new Country() { id_continent = 7, name = "Sweden" });
            cr.SaveCountry(new Country() { id_continent = 7, name = "Finland" });
            cr.SaveCountry(new Country() { id_continent = 7, name = "Francia" });
        }

        public static void AddCities()
        {
            CityRepositories cr = new CityRepositories();
            cr.SaveCity(new City() { id_country = 1, name = "New York" });
            cr.SaveCity(new City() { id_country = 7, name = "Sydney" });
            cr.SaveCity(new City() { id_country = 8, name = "Berlin" });
            cr.SaveCity(new City() { id_country = 9, name = "Rome" });
            cr.SaveCity(new City() { id_country = 10, name = "Stockgolm" });
            cr.SaveCity(new City() { id_country = 11, name = "Helsinki" });
            cr.SaveCity(new City() { id_country = 12, name = "Paris" });
        }

        static void Main(string[] args)
        {
            ContinentRepository continentRepo = new ContinentRepository();
            CountryRepositories countryRepo = new CountryRepositories();
            CityRepositories cityRepo = new CityRepositories();
            //AddContinents();
            //AddCountries();
            //AddCities();
            Console.WriteLine("***Continents:");
            continentRepo.Print();

            Console.WriteLine("***Countries:");
            countryRepo.Print();

            Console.WriteLine("***Cities:");
            cityRepo.Print();

            City city = cityRepo.Cities.FirstOrDefault(c => c.name == "Berlin");
            Console.WriteLine("Find city: {0}", city);

            Console.ReadLine();
        }
    }
}
