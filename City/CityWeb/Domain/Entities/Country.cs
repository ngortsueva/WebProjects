using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityWeb.Domain.Entities
{
    public class Country
    {
        public int Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Continent { get; set; }

        public string Holidays { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2} {3} {4}", Id, Code, Name, Continent, Holidays);
        }
    }
}
