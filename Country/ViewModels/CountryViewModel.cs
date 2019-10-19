using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CountryWeb.Models;

namespace CountryWeb.ViewModels
{
    public class CountryViewModel
    {
        public Country Country { get; set; }
        public SelectList Continents { get; set; }
        public int? SelectedContinent { get; set; }
    }
}
