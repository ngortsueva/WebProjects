using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using CountryWeb.Models;

namespace CountryWeb.ViewModels
{
    public class AddressViewModel
    {
        public Address Address { get; set; }
        public SelectList Streets { get; set; }
        public SelectList Countries { get; set; }
        public SelectList Regions { get; set; }
        public SelectList Cities { get; set; }
        public int SelectedCountry { get; set; }
        public int SelectedRegion { get; set; }
        public int SelectedCity { get; set; }
        public int SelectedStreet { get; set; }
    }
}
