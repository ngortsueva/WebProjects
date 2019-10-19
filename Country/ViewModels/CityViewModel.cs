using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CountryWeb.Models;

namespace CountryWeb.ViewModels
{
    public class CityViewModel
    {
        public City City { get; set; }        
        public SelectList Countries { get; set; }
        public SelectList Regions { get; set; }
        public int SelectedCountry { get; set; }
        public int SelectedRegion { get; set; }
    }
}
