using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BirthdayWeb.Domain.Abstract;

namespace BirthdayWeb.Controllers
{
    public class LogsController : Controller
    {
        private ILogRepository log;

        public LogsController(ILogRepository log)
        {
            this.log = log;
        }

        public IActionResult Index()
        {
            return View(log.Messages);
        }

        public IActionResult Clear()
        {
            log.Clear();

            return RedirectToAction("Index");
        }
    }
}