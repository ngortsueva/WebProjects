using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSql
{
    public class Country
    {
        public int CountryID { get; set; }
        public int ContintentID { get; set; }
        public string Name { get; set; }

        public Country() { }
        public Country(int id, int id_continent, string name)
        {
            CountryID = id;
            ContintentID = id_continent;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", CountryID, ContintentID, Name);
        }
    }
}
