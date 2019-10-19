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
    public class ContinentRepository : IContinentRepository
    {
        private string strConnection;
        private SqlConnection cn = null;

        public ContinentRepository(string connection)
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

        public void Add(Continent continent)
        {
            string sql = string.Format("Insert Into Continents (id, name) Values('{0}', '{1}')",
                                        continent.ContinentID,
                                        continent.Name);
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

        public void Delete(Continent continent)
        {
            string sql = string.Format("Delete from Continents where id = '{0}'", continent.ContinentID);
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

        public void Update(Continent continent)
        {
            string sql = string.Format("Update Continents Set name = '{0}' Where id = '{1}'", continent.Name, continent.ContinentID);

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
            string sql = string.Format("Select * from Continents");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Console.WriteLine("{0} {1}", reader["id"], reader["name"]);
                    }

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
        }

        public Continent Find(int id)
        {
            Continent continent = new Continent();
            string sql = string.Format("Select * from Continents where id = '{0}'", id);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            continent.ContinentID = int.Parse(reader["id"].ToString());
                            continent.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else continent = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return continent;
        }

        public Continent Find(string name)
        {
            Continent continent = new Continent();
            string sql = string.Format("Select * from Continents where name = '{0}'", name);
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            continent.ContinentID = int.Parse(reader["id"].ToString());
                            continent.Name = reader["name"].ToString();
                            break;
                        }
                    }
                    else continent = null;

                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return continent;
        }

        public IList<Continent> GetContinentList()
        {
            Continent continent;
            List<Continent> continents = new List<Continent>();
            
            string sql = string.Format("Select * from Continents");
            try
            {
                using (SqlCommand cmd = new SqlCommand(sql, cn))
                {
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        continent = new Continent();
                        continent.ContinentID = int.Parse(reader["id"].ToString());
                        continent.Name = reader["name"].ToString();

                        continents.Add(continent);
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Print command error: {0}", ex.Message);
            }
            return continents;
        }
    }
}
