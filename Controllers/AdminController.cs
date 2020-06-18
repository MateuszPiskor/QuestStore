using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
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
            AdminOperations = new AdminOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db4"));
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
        public IActionResult AddClassForm()
        {
            AddClassFormViewModel addClassFormViewModel = new AddClassFormViewModel();
            addClassFormViewModel.Mentors = AdminOperations.GetMentors();
            //addClassFormViewModel.Cities=AdminOperations.GetCities();
            return View(addClassFormViewModel);
        }

        [HttpPost]
        public IActionResult AddClassForm(AddClassFormViewModel
        addClassFormViewModel)
        {
            TempData["Message"] = "Class has been added";
            AdminOperations.AddClass(addClassFormViewModel.Class);
            int classId = AdminOperations.GetMaxClassId();
            List<User> mentors = addClassFormViewModel.Mentors;
            int selectedMentors = 0;
            foreach (User mentor in mentors)
            {
                if (mentor.IsChecked == true)
                {
                    selectedMentors += 1;
                }
            }
            if (selectedMentors > 0)
            {
                List<int> mentorsIds = new List<int>();
                foreach (User mentor in mentors)
                {
                    if (mentor.IsChecked == true)
                    {
                        mentorsIds.Add(mentor.Id);
                    }
                }
                AdminOperations.AddClassMentors(mentorsIds, classId);
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
            EditClassFormViewModel editClassFormViewModel = new EditClassFormViewModel();
            editClassFormViewModel.Class = AdminOperations.getClassByClassId(id);
            editClassFormViewModel.Class.Id = id;
            editClassFormViewModel.Mentors = AdminOperations.GetMentors();
            editClassFormViewModel.ClassMentors = AdminOperations.GetMentorsByClassId(id);
            markMentorsAsClassMentors(editClassFormViewModel.Mentors, editClassFormViewModel.ClassMentors);

            editClassFormViewModel.Cities = AdminOperations.GetCities();
            return View(editClassFormViewModel);
        }

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

        public IActionResult EditExpierenceLevelForm(int id)
        {
            ExpLevel expLevel = AdminOperations.GetLevelById(id);
            return View(expLevel);
        }

        public IActionResult ClassProfileView(int id)
        {
            ClassProfileViewModel classProfileViewModel = new ClassProfileViewModel();
            classProfileViewModel.Class = AdminOperations.getClassByClassId(id);
            classProfileViewModel.Mentors = AdminOperations.GetMentorsByClassId(id);
            return View(classProfileViewModel);
        }
    }
}
