using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsAdmin;

namespace Queststore.Controllers
{
    public class AdminController : Controller
    {
        IAdmin AdminOperations;
        public AdminController()
        {
            AdminOperations = new AdminOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db"));
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult AddMentorForm()
        {
            ViewModelMentorClasses mentorAndClasses = new ViewModelMentorClasses();
            mentorAndClasses.Classes = AdminOperations.GetClasses();
            return View(mentorAndClasses);
        }
        [HttpPost]
        public IActionResult AddMentorForm(ViewModelMentorClasses mentorAndClasses)
        {
            AdminOperations.AddMentor(mentorAndClasses.Mentor);
            mentorAndClasses.Mentor.Id = AdminOperations.GetMaxMentorId();
            AdminOperations.AddClassMentor(mentorAndClasses.ClassId, mentorAndClasses.Mentor.Id);

            //AdminOperations.AddMentorClass(mentorAndClasses.Mentor.Id, mentorAndClasses.ClassId);
            return RedirectToAction("AddMentorForm", "Admin");
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
            return RedirectToAction("Admin", "ExpLevelsList");
        }

        public IActionResult MentorsList()
        {
            List<User> admins = AdminOperations.GetMentors();
            return View(admins);
        }

        public IActionResult ClassesList()
        {
            List<Class> classes = AdminOperations.GetClasses();
            return View(classes);
        }

        public IActionResult ExpLevelsList()
        {
            IEnumerable<ExpLevel> levels = AdminOperations.ExpLevelsList();
            return View(levels);
        }

        public IActionResult MentorProfileView(int id)
        {
            ViewModelMentorClasses mentorAndClasses = new ViewModelMentorClasses();
            mentorAndClasses.Mentor.Id = id;
            mentorAndClasses.Classes = AdminOperations.GetClassesByUserId(id);
            mentorAndClasses.Mentor = AdminOperations.GetUserById(id);
            return View(mentorAndClasses);
        }
        public IActionResult EditMentorForm(int id)
        {
            ViewModelMentorClasses mentorAndClasses = new ViewModelMentorClasses();
            //mentorAndClasses.Mentor.Id = id;
            mentorAndClasses.Classes = AdminOperations.GetClasses();
            mentorAndClasses.Mentor = AdminOperations.GetUserById(id);
            return View(mentorAndClasses);
        }
        [HttpPost]
        public IActionResult EditMentorForm(ViewModelMentorClasses mentorAndClasses)
        {
            AdminOperations.EditMentor(mentorAndClasses.Mentor.Id, mentorAndClasses.Mentor);
            TempData["Message"] = "You have edited mentor details!";
            return RedirectToAction("Index");
        }

        public IActionResult EditClassForm(int id)
        {
            ViewModelMentorsClass mentorsAndClass = new ViewModelMentorsClass();
            mentorsAndClass.Class = AdminOperations.getClassByClassId(id);
            mentorsAndClass.Class.Id = id;
            mentorsAndClass.Mentors=AdminOperations.GetMentors();
            mentorsAndClass.Cities= AdminOperations.GetCities();
            return View(mentorsAndClass);
        }
        [HttpPost]
        public IActionResult EditClassForm(ViewModelMentorsClass mentorsAndClass)
        {
            AdminOperations.EditClass(mentorsAndClass.Class);
            return RedirectToAction("Index");
        }

        public IActionResult EditExpierenceLevelForm(int id)
        {
            ExpLevel expLevel = AdminOperations.GetLevelById(id);
            return View(expLevel);
        }

    }
}
