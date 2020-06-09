using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Queststore.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddMentor()
        {
            return View();
        }
        public IActionResult AddClass()
        {
            return View();
        }
        public IActionResult AddLevel()
        {
            return View();
        }
    }
}
