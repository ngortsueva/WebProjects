using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityWeb.Domain.Entities
{
    public class City
    {
        public int Id { get; set; }
        public int Country { get; set; }
        public string Name { get; set; }

        public override string ToString()
        {
            return string.Format("{0} {1} {2}", Id, Country, Name);
        }
    }
}
