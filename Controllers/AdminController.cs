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
        [HttpGet]
        public IActionResult AddClassForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClassForm(Class group)
        {
            AdminOperations.AddClass(group);
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
            TempData["Message"] = "You have addedd the level!";
            return RedirectToAction("ExpLevelsList", "Admin");
        }
        [HttpPost]
        public IActionResult EditExpierenceLevelForm(ExpLevel expLevel)
        {
            TempData["Message"] = "You have changed level details!";
            AdminOperations.EditExpierenceLevelForm(expLevel);
            return RedirectToAction("ExpLevelsList", "Admin");
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
            IEnumerable<ExpLevel> levels=AdminOperations.ExpLevelsList();     
            return View(levels);
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

        public IActionResult EditExpierenceLevelForm(int id)
        {
            ExpLevel expLevel=AdminOperations.GetLevelById(id);
            return View(expLevel);
        }

    }
}
