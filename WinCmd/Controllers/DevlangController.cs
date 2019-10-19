using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WinCmd.Controllers
{
    public class DevLangController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}