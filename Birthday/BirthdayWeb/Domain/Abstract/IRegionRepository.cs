using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BirthdayWeb.Models;

namespace BirthdayWeb.Domain.Abstract
{
    public interface IRegionRepository
    {
        IQueryable<Region> Regions { get; }
        void SaveRegion(Region region);
        void DeleteRegion(Region region);
        void DeleteRegion(int id);
    }
}
