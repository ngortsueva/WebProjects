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
    public class CityRepository : ICityRepository
    {
        private string strConnection;
        private SqlConnection cn = null;

        public CityRepository(string connection)
        {
            strConnection = connection;
        }

        public void Open()
        {
            cn = new SqlConnection(strConnection);
            cn.Open();
        }

        public void Close()
        {
            cn.Close();
        }

        public void Add(City city)
        {
            string sql = string.Format("Insert Into Cities (id, id_country, name) Values('{0}', '{1}', '{2}')", city.CityID, city.CountryID, city.Name);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Delete(City city)
        {
            string sql = string.Format("Delete from Cities where id = '{0}'", city.CityID);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void Update(City city)
        {
            string sql = string.Format("Update Cities Set name = '{0}' Where id = '{1}'", city.Name, city.CityID);

            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Update command error: {0}", ex.Message);
            }
        }

        public void Save() { }

        public void Print()
        {
            string sql = string.Format("Select Cities.id, Cities.name as city, Countries.name as country, Continents.name as continent " + 
                                       "From Cities LEFT JOIN Countries ON City.id_country = Country.id " +
                                                  "LEFT JOIN Continents ON Country.id_continent = Continent.id");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1} {2} {3}", reader["id"], reader["city"], reader["country"], reader["continent"]);
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
        }

        public City Find(int id)
        {
            City city = new City();
            string sql = string.Format("Select * from Cities where id = '{0}'", id);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            city.CityID = int.Parse(reader["id"].ToString());
                            city.CountryID = int.Parse(reader["id_country"].ToString());
                            city.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else city = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return city;
        }

        public City Find(string name)
        {
            City city = new City();
            string sql = string.Format("Select * from Cities where name = '{0}'", name);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            city.CityID = int.Parse(reader["id"].ToString());
                            city.CountryID = int.Parse(reader["id_country"].ToString());
                            city.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else city = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return city;
        }

        public IList<City> GetCityList()
        {
            City city;
            List<City> cities = new List<City>();
                         
            string sql = string.Format("Select * from Cities");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        city = new City();
                        city.CityID = int.Parse(reader["id"].ToString());
                        city.CountryID = int.Parse(reader["id_country"].ToString());
                        city.Name = reader["name"].ToString();

                        cities.Add(city);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return cities;
        }
    }
}
