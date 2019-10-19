using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleSql
{
    public class Continent
    {
        public int ContinentID { get; set; }
        public string Name { get; set; }

        public Continent() { }

        public Continent(int id, string name)
        {
            ContinentID = id;
            Name = name;
        }

        public override string ToString()
        {
            return string.Format("{0} {1}", ContinentID, Name);
        }
    }
}
