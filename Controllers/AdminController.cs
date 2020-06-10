using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;

namespace Queststore.Controllers
{
    public class AdminController : Controller
    {
        IAdmin AdminOperations;
        public AdminController()
        {
            AdminOperations=new AdminOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db"));
        }
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
        [HttpPost]
        public IActionResult AddLevelForm(ExpLevel expLevel)
        {
            AdminOperations.AddLevelForm(expLevel);
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

        public IActionResult EditExpierenceLevelForm()
        {
            return View();
        }

    }
}
