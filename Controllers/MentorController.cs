﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsMentor;

namespace Queststore.Controllers
{
    public class MentorController : Controller
    {
        private readonly IMentor _mentorOperationsFromDB;
        private ISessionManager _sessionManager;
        //private int _loggedMentorId => Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
        //private string _loggedUserRole => Request.Cookies["UserRole"];
        private string _expectedUserRole = "Mentor";

        public MentorController()
        {
            _mentorOperationsFromDB = new MentorOperationsFromDB(new DataBaseConnection("localhost", "agnieszkachruszczyksilva", "startthis", "queststore"));
            //_mentorOperationsFromDB = new MentorOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db4"));
            _sessionManager = new SessionManager(new HttpContextAccessor());
        }


        [HttpGet]
        public IActionResult Index()
        {
            IActionResult view = View();
            return ConfirmUserRoleWithAccountAndDisplayView(view);
        }

        private IActionResult ConfirmUserRoleWithAccountAndDisplayView(IActionResult view)
        {
            if (IsLoggedUserExpectedUser() && IsAnyUserLogged())
            {
                return view;
            }
            else if (!IsLoggedUserExpectedUser() && IsAnyUserLogged())
            {
                TempData["Message"] = $"You have no access to {_expectedUserRole} account.";
                return RedirectToAction("Index", $"{_sessionManager.LoggedUserRole}");
            }
            return RedirectToAction("Index", "Login");
        }

        private bool IsLoggedUserExpectedUser()
        {
            return _sessionManager.LoggedUserRole == _expectedUserRole;
        }

        private bool IsAnyUserLogged()
        {
            return _sessionManager.LoggedUserId != 0;
        }

        [HttpGet]
        public IActionResult Students()
        {
            int _loggedMentorId = Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
            var studentsAndClasses = _mentorOperationsFromDB.GetStudentsByMentorAndClassId(_loggedMentorId, 0);
            return View(studentsAndClasses);

        }
        [HttpPost]
        public IActionResult Students(int classId)
        {
            int _loggedMentorId = Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
            if (classId == 0)
            {
                return RedirectToAction("Students");
            }
            var studentsAndClasses = _mentorOperationsFromDB.GetStudentsByMentorAndClassId(_loggedMentorId, classId);
            return View(studentsAndClasses);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            int _loggedMentorId = Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
            ViewModelStudentClasses studentAndClasses = new ViewModelStudentClasses();
            studentAndClasses.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentorId);
            return View(studentAndClasses);
        }

        [HttpPost]
        public IActionResult AddStudent(ViewModelStudentClasses studentAndClasses)
        {
            _mentorOperationsFromDB.AddStudent(studentAndClasses.Student, studentAndClasses.ClassId);
            studentAndClasses.Student.Id = _mentorOperationsFromDB.GetMaxStudentId();
            _mentorOperationsFromDB.AddUser(studentAndClasses.Student);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddTeam()
        {
            int _loggedMentorId = Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
            ViewModelAddTeam viewModelAddTeam = new ViewModelAddTeam();
            viewModelAddTeam.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentorId);
            return View(viewModelAddTeam);
        }

        [HttpPost]
        public IActionResult AddTeam(int classId)
        {
            int _loggedMentorId = Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));
            ViewModelAddTeam viewModelAddTeam = new ViewModelAddTeam();
            viewModelAddTeam.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentorId);
            viewModelAddTeam.ClassId = classId;
            viewModelAddTeam.Students = _mentorOperationsFromDB.GetStudentsByClassId(classId);
            return View(viewModelAddTeam);
        }

        [HttpGet]
        public IActionResult Quests()
        {
            ViewModelQuests questsAndSelectedQuest = new ViewModelQuests();
            questsAndSelectedQuest.QuestTypes = _mentorOperationsFromDB.GetQuestTypes();
            questsAndSelectedQuest.Quests = _mentorOperationsFromDB.GetQuestsByType("");
            return View(questsAndSelectedQuest);
        }

        [HttpPost]
        public IActionResult Quests(string questName)
        {
            ViewModelQuests questsAndSelectedQuest = new ViewModelQuests();
            if (!String.IsNullOrEmpty(questName))
            {
                questsAndSelectedQuest.QuestTypes = _mentorOperationsFromDB.GetQuestTypes();
                questsAndSelectedQuest.Quests = _mentorOperationsFromDB.GetQuestsByType(questName);
                return View(questsAndSelectedQuest);
            }
            return RedirectToAction("Quests");
        }

        [HttpGet]
        public IActionResult AddQuest()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddQuest(Quest quest)
        {
            _mentorOperationsFromDB.AddQuest(quest);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddArtifact()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddArtifact(Artifact artifact)
        {
            _mentorOperationsFromDB.AddArtifact(artifact);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult ViewStudentProfile(int id)
        {
            Student student = _mentorOperationsFromDB.GetStudentById(id);
            return View(student);
        }

        [HttpGet]
        public IActionResult ViewStudentWallet(int id)
        {
            Student student = _mentorOperationsFromDB.GetStudentById(id);
            return View(student);
        }

        [HttpGet]
        public IActionResult MarkQuest(int id)
        {
            ViewModelMarkQuest viewModelMarkQuest = new ViewModelMarkQuest();
            viewModelMarkQuest.QuestTypes = _mentorOperationsFromDB.GetQuestTypes();
            viewModelMarkQuest.Student = _mentorOperationsFromDB.GetStudentById(id);
            return View(viewModelMarkQuest);
        }

        [HttpPost]
        public IActionResult MarkQuest(string questType, int id, List<Quest> quests)
        {
            ViewModelMarkQuest viewModelMarkQuest = new ViewModelMarkQuest();
            viewModelMarkQuest.QuestTypes = _mentorOperationsFromDB.GetQuestTypes();
            viewModelMarkQuest.QuestType = questType;
            viewModelMarkQuest.Student = _mentorOperationsFromDB.GetStudentById(id);
            viewModelMarkQuest.Quests = _mentorOperationsFromDB.GetQuestsByType(questType);
            if (quests.All(item => item.IsChecked == false))
            {
                return View(viewModelMarkQuest);
            }
            else
            {
                int checkedItems = quests.Count(item => item.IsChecked == true);
                if (checkedItems > 1)
                {
                    return View("Index"); //add Error message (to select only one quest)
                }
                else
                {
                    Quest selectedQuest = quests.FirstOrDefault(item => item.IsChecked == true);
                    _mentorOperationsFromDB.MarkQuest(id, selectedQuest.Id);
                    viewModelMarkQuest.Student.Coolcoins += selectedQuest.Value;
                    _mentorOperationsFromDB.UpdateStudentCoolcoins(viewModelMarkQuest.Student.Id, viewModelMarkQuest.Student.Coolcoins);
                    return RedirectToAction("Index"); //add date of quest mark to db and here
                }
                
            }

        }

        [HttpGet]
        public IActionResult Error()
        {
            var message = TempData["Message"];
            TempData.Remove("Message");
            return View(message);
        }
    }
}
