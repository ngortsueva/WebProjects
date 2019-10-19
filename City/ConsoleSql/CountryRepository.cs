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
    public class CountryRepository : ICountryRepository
    {
        private string strConnection;
        private SqlConnection cn = null;

        public CountryRepository(string connection)
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

        public void Add(Country country)
        {
            string sql = string.Format("Insert Into Countries (id, id_continent, name) Values('{0}', '{1}', '{2}')", 
                                        country.CountryID, 
                                        country.ContintentID,
                                        country.Name);
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

        public void Delete(Country country)
        {
            string sql = string.Format("Delete from Countries where id = '{0}'", country.CountryID);
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

        public void Update(Country country)
        {
            string sql = string.Format("Update Countries Set name = '{0}' Where id = '{1}'", country.Name, country.CountryID);

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
            string sql = string.Format("Select Countries.id, Countries.name as country, Continents.name as continent From Countries JOIN Continents " +
                                       "ON Country.id_continent = Continent.id");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1} {2}", reader["id"], reader["country"], reader["continent"]);
                            
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
        }

        public Country Find(int id)
        {
            Country country = new Country();
            string sql = string.Format("Select * from Countries where id = '{0}'", id);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            country.CountryID = int.Parse(reader["id"].ToString());
                            country.ContintentID = int.Parse(reader["id_continent"].ToString());
                            country.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else country = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return country;
        }

        public Country Find(string name)
        {
            Country country = new Country();
            string sql = string.Format("Select * from Countries where name = '{0}'", name);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            country.CountryID = int.Parse(reader["id"].ToString());
                            country.ContintentID = int.Parse(reader["id_continent"].ToString());
                            country.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else country = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }

            return country;
        }

        public IList<Country> GetCountryList()
        {
            Country country;
            List<Country> countries = new List<Country>();
            
            string sql = string.Format("Select * from Countries");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        country = new Country();
                        country.CountryID = int.Parse(reader["id"].ToString());
                        country.ContintentID = int.Parse(reader["id_continent"].ToString());
                        country.Name = reader["name"].ToString();

                        countries.Add(country);
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }

            return countries;
        }
    }
}
