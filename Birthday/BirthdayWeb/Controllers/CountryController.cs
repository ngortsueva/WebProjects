using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using BirthdayWeb.Domain.Abstract;
using BirthdayWeb.Domain;
using BirthdayWeb.Models;

namespace BirthdayWeb.Controllers
{
    public class CountryController : Controller
    {
        private ICountryRepository countryRepository;
        private IRegionRepository regionRepository;

        public CountryController(ICountryRepository countryRepo, IRegionRepository regionRepo)
        {
            countryRepository = countryRepo;
            regionRepository = regionRepo;
        }

        public IActionResult Index()
        {
            return View(countryRepository.Countries);
        }
    }
}