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
            AddMentorFormViewModel addMentorFormViewModel = new AddMentorFormViewModel();
            addMentorFormViewModel.Classes = AdminOperations.GetClasses();
            return View(addMentorFormViewModel);
        }
        [HttpPost]
        public IActionResult AddMentorForm(AddMentorFormViewModel addMentorFormViewModel)
        {
            TempData["Message"] = "Mentor has been added";
            AdminOperations.AddMentor(addMentorFormViewModel.Mentor);
            addMentorFormViewModel.Mentor.Id = AdminOperations.GetMaxMentorId();
            List<Class> classes = addMentorFormViewModel.Classes;
            int selectedClasses = 0;
            foreach (var group in classes)
            {
                if (group.IsChecked == true)
                {
                    selectedClasses += 1;
                }
            }
            if (selectedClasses > 0)
            {
                List<int> classesIds = new List<int>();
                foreach (var group in classes)
                {
                    if (group.IsChecked == true)
                    {
                        classesIds.Add(group.Id);
                    }
                }
                AdminOperations.AddClassMentor(classesIds, addMentorFormViewModel.Mentor.Id);
            }
            return RedirectToAction("Index", "Admin");

            //AdminOperations.AddMentor(mentorAndClasses.Mentor);
            //mentorAndClasses.Mentor.Id = AdminOperations.GetMaxMentorId();
            //if (mentorAndClasses.ClassId != 0)
            //{
            //    AdminOperations.AddClassMentor(mentorAndClasses.ClassId, mentorAndClasses.Mentor.Id);
            //}
            //return RedirectToAction("AddMentorForm", "Admin");
        }

        [HttpGet]
        public IActionResult AddClassForm()
        {
            AddClassFormViewModel addClassFormViewModel = new AddClassFormViewModel();
            addClassFormViewModel.Mentors = AdminOperations.GetMentors();
            return View(addClassFormViewModel);
        }

        [HttpPost]
        public IActionResult AddClassForm(AddClassFormViewModel
        addClassFormViewModel)
        {
            TempData["Message"] = "Class has been added";
            AdminOperations.AddClass(addClassFormViewModel.Class);
            int classId= AdminOperations.GetMaxClassId();
            List<User> mentors = addClassFormViewModel.Mentors;
            int selectedMentors = 0;
            foreach(var mentor in mentors)
            {
                if (mentor.IsChecked == true)
                {
                    selectedMentors += 1;
                }
            }
            if (selectedMentors > 0)
            {
                List<int> mentorsIds = new List<int>();
                foreach(var mentor in mentors)
                {
                    if (mentor.IsChecked == true)
                    {
                    mentorsIds.Add(mentor.Id);
                    }
                }
                AdminOperations.AddClassMentor(mentorsIds, classId);
            }
            return RedirectToAction("Index", "Admin");
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
            mentorsAndClass.Mentors = AdminOperations.GetMentors();
            mentorsAndClass.Cities = AdminOperations.GetCities();
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
