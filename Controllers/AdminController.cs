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

        public IActionResult AddMentorForm()
        {
            return View();
        }

        public IActionResult AddClassForm()
        {
            return View();
        }

        public IActionResult AddLevelForm()
        {
            return View();
        }

        public IActionResult MentorsList()
        {
            return View();
        }

        public IActionResult ClassesList()
        {
            return View();
        }

        public IActionResult ExpLevelsList()
        {
            return View();
        }

        public IActionResult MentorProfileView()
        {
            return View();
        }
        public IActionResult EditMentorForm()
        {
            return View();
        }
        
        public IActionResult EditClassForm()
        {
            return View();
        }

        public IActionResult ClassProfileView()
        {
            return View();
        }

    }
}
