﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BirthdayWeb.Models
{
    public class Country
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Region> Regions { get; set; }
    }

    public class Region
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }        
        public Country Country { get; set; }        
    }

    public class City
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        public List<Street> Streets { get; set; }
    }

    public class Street
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public City City { get; set; }
        [NotMapped]
        public int[] Houses { get; set; }
    }

    public class Address
    {
        [Key]
        public int Id { get; set; }
        public Country Country { get; set; }
        public Region Region { get; set; }
        public City City { get; set; }
        public Street Street { get; set; }
        public string HouseNumber { get; set; }
    }

    public class Contact
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string OwnSiteUrl { get; set; }
        public Address Address { get; set; }
    }
}