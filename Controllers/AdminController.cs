using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsAdmin;

namespace Queststore.Controllers
{
    public class AdminController : Controller
    {
        public IAdmin AdminOperations { get; set; }
        private int _loggedAdminId => Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));

        public AdminController()
        {
            //AdminOperations = new AdminOperationsFromDB(new DataBaseConnection("localhost", "agnieszkachruszczyksilva", "startthis", "queststore"));
            AdminOperations = new AdminOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db4"));
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
            if (ModelState.IsValid)
            {
                AdminOperations.AddClass(addClassFormViewModel.Class);
                //TempData["Message"] = "Class has been added";
                if (getSelectedMentors(addClassFormViewModel.Mentors) > 0)
                {
                    AddClassMentors(addClassFormViewModel);
                }
                //AddClassMentors(addClassFormViewModel, selectedMentors);
                return RedirectToAction("Index", "Admin");
            }
            return View();
        }

        private void AddClassMentors(AddClassFormViewModel addClassFormViewModel)
        {
            List<int> mentorsIds = new List<int>();
            foreach (User mentor in addClassFormViewModel.Mentors)
            {
                if (mentor.IsChecked == true)
                {
                    mentorsIds.Add(mentor.Id);
                }
            }

            int classId = AdminOperations.GetMaxClassId();
            AdminOperations.AddClassMentors(mentorsIds, classId);
        }

        private int getSelectedMentors(List<User> mentors)
        {
            int selectedMentors = 0;
            foreach (User mentor in mentors)
            {
                if (mentor.IsChecked == true)
                {
                    selectedMentors += 1;
                }
            }
            return selectedMentors;
        }

        [HttpGet]
        public IActionResult AddLevelForm()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddLevelForm(ExpLevel expLevel)
        {
            if (ModelState.IsValid)
            {
                AdminOperations.AddLevel(expLevel);
                return RedirectToAction("ExpLevelsList", "Admin");
            }
            return View();
            //TempData["Message"] = "You have addedd the level!";
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
            foreach (Class group in classes)
            {
                if (group.IsChecked == true)
                {
                    selectedClasses += 1;
                }
            }
            if (selectedClasses > 0)
            {
                List<int> classesIds = new List<int>();
                foreach (Class group in classes)
                {
                    if (group.IsChecked == true)
                    {
                        classesIds.Add(group.Id);
                    }
                }
                AdminOperations.AddClassesMentor(classesIds, addMentorFormViewModel.Mentor.Id);
            }
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult ClassesList()
        {
            List<Class> classes = AdminOperations.GetClasses();
            return View(classes);
        }

        [HttpGet]
        public IActionResult ClassProfileView(int? id)
        {
            //throw new Exception("Error in class profile view");
            if (id != null)
            {
                ClassProfileViewModel classProfileViewModel = new ClassProfileViewModel();
                classProfileViewModel.Class = AdminOperations.getClassByClassId(id.Value);
                classProfileViewModel.Mentors = AdminOperations.GetMentorsByClassId(id.Value);
                return View(classProfileViewModel);
            }
            return RedirectToAction("ClassesList");
        }

        [HttpGet]
        public IActionResult EditClassForm(int id)
        {
            EditClassFormViewModel editClassFormViewModel = new EditClassFormViewModel();
            editClassFormViewModel.Class = AdminOperations.getClassByClassId(id);
            editClassFormViewModel.Class.Id = id;
            editClassFormViewModel.Mentors = AdminOperations.GetMentors();
            editClassFormViewModel.ClassMentors = AdminOperations.GetMentorsByClassId(id);
            markMentorsAsClassMentors(editClassFormViewModel.Mentors, editClassFormViewModel.ClassMentors);

            editClassFormViewModel.Cities = AdminOperations.GetCities();
            return View(editClassFormViewModel);
        }

        [HttpPost]
        public IActionResult EditClassForm(EditClassFormViewModel editClassFormViewModel)
        {
            TempData["Message"] = "Class has been edited";
            AdminOperations.RemoveAllMentorsFromCurrentClass(editClassFormViewModel.Class.Id);
            AdminOperations.EditClass(editClassFormViewModel.Class);
            List<int> mentorsIds = GetClassMentorsIds(editClassFormViewModel.Mentors);
            AdminOperations.AddClassMentors(mentorsIds, editClassFormViewModel.Class.Id);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult EditExpierenceLevelForm(ExpLevel expLevel)
        {
            TempData["Message"] = "You have changed level details!";
            AdminOperations.EditExpierenceLevelForm(expLevel);
            return RedirectToAction("Admin", "ExpLevelsList");
        }

        [HttpGet]
        public IActionResult EditExpierenceLevelForm(int id)
        {
            ExpLevel expLevel = AdminOperations.GetLevelById(id);
            return View(expLevel);
        }

        [HttpGet]
        public IActionResult EditMentorForm(int id)
        {
            EditMentorFormViewModel editMentorFormViewModel = new EditMentorFormViewModel();
            editMentorFormViewModel.Mentor = AdminOperations.GetUserById(id);
            editMentorFormViewModel.Mentor.Id = id;
            editMentorFormViewModel.Classes = AdminOperations.GetClasses();
            editMentorFormViewModel.MentorClasses = AdminOperations.GetClassesByMentorId(id);
            markClassesAsMentorClass(editMentorFormViewModel.Classes, editMentorFormViewModel.MentorClasses);

            //editMentorFormViewModel.Cities = AdminOperations.GetCities();
            return View(editMentorFormViewModel);

            //ViewModelMentorClasses mentorAndClasses = new ViewModelMentorClasses();
            //mentorAndClasses.Classes = AdminOperations.GetClasses();
            //mentorAndClasses.Mentor = AdminOperations.GetUserById(id);
            //return View(mentorAndClasses);
        }

        [HttpPost]
        public IActionResult EditMentorForm(EditMentorFormViewModel editMentorFormViewModel)
        {
            TempData["Message"] = "Mentor has been edited";
            AdminOperations.RemoveAllClassesToCurrentMentor(editMentorFormViewModel.Mentor.Id);
            AdminOperations.EditMentor(editMentorFormViewModel.Mentor);
            List<int> classesIds = GetMentorClassesIds(editMentorFormViewModel.Classes);
            //AdminOperations.EditMentor(mentorAndClasses.Mentor.Id, mentorAndClasses.Mentor);
            AdminOperations.AddMentorClasses(classesIds, editMentorFormViewModel.Mentor.Id);
            return RedirectToAction("Index");

            //TempData["Message"] = "Class has been edited";
            //AdminOperations.RemoveAllMentorsFromCurrentClass(editClassFormViewModel.Class.Id);
            //AdminOperations.EditClass(editClassFormViewModel.Class);
            //List<int> mentorsIds = GetClassMentorsIds(editClassFormViewModel.Mentors);
            //AdminOperations.AddClassMentors(mentorsIds, editClassFormViewModel.Class.Id);
            //return RedirectToAction("Index");

            //TempData["Message"] = "You have edited mentor details!";
            //return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ExpLevelsList()
        {
            IEnumerable<ExpLevel> levels = AdminOperations.ExpLevelsList();
            return View(levels);
        }

        [HttpGet]
        public IActionResult EditUser() //id from cookie/session
        {
            User user = AdminOperations.GetUserById(_loggedAdminId);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User user)
        {
            TempData["Message"] = "Your personal detalis has been upadted";
            AdminOperations.EditMentor(user);
            return RedirectToAction("Index", "Admin");
        }

        [HttpGet]
        public IActionResult GoToFacebook()
        {
            return Redirect("http://www.facebook.com");
        }

        [HttpGet]
        public IActionResult GoToTwitter()
        {
            return Redirect("http://www.twitter.com");
        }

        [HttpGet]
        public IActionResult GoToYoutube()
        {
            return Redirect("http://www.youtube.com");
        }

        [HttpGet]
        public IActionResult GoToInstagram()
        {
            return Redirect("http://www.instagram.com");
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ControllerName = "Admin";
            IndexViewModel indexViewModel = new IndexViewModel();
            indexViewModel.Class = AdminOperations.GetLastAddedClass();
            indexViewModel.Level = AdminOperations.GetLastAddedLevel();
            indexViewModel.User = AdminOperations.GetLastUser();
            return View(indexViewModel);
        }

        [HttpGet]
        public IActionResult MentorProfileView(int id)
        {
            ViewModelMentorClasses mentorAndClasses = new ViewModelMentorClasses();
            mentorAndClasses.Mentor.Id = id;
            mentorAndClasses.Classes = AdminOperations.GetClassesByUserId(id);
            mentorAndClasses.Mentor = AdminOperations.GetUserById(id);
            return View(mentorAndClasses);
        }

        [HttpGet]
        public IActionResult MentorsList()
        {
            List<User> admins = AdminOperations.GetMentors();
            return View(admins);
        }

        private List<int> GetClassMentorsIds(List<User> mentors)
        {
            List<int> ids = new List<int>();
            foreach (User mentor in mentors)
            {
                if (mentor.IsChecked == true)
                {
                    ids.Add(mentor.Id);
                }
            }
            return ids;
        }

        [HttpGet]
        private List<int> GetMentorClassesIds(List<Class> classes)
        {
            List<int> ids = new List<int>();
            foreach (Class @class in classes)
            {
                if (@class.IsChecked == true)
                {
                    ids.Add(@class.Id);
                }
            }
            return ids;
        }

        [HttpGet]
        private void markClassesAsMentorClass(List<Class> classes, List<Class> mentorClasses)
        {
            foreach (Class @class in classes)
            {
                foreach (Class previus in mentorClasses)
                {
                    if (@class.Id == previus.Id)
                    {
                        @class.PreviusChecked = true;
                    }
                }
            }
        }

        [HttpGet]
        private void markMentorsAsClassMentors(List<User> mentors, List<User> ClassMentors)
        {
            foreach (User mentor in mentors)
            {
                foreach (User previus in ClassMentors)
                {
                    if (mentor.Id == previus.Id)
                    {
                        mentor.PreviusChecked = true;
                    }
                }
            }
        }
    }
}