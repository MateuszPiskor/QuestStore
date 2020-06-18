using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
        private readonly User _loggedMentor;

        public MentorController()
        {
            _mentorOperationsFromDB = new MentorOperationsFromDB(new DataBaseConnection("localhost", "agnieszkachruszczyksilva", "startthis", "queststore"));
            //_mentorOperationsFromDB = new MentorOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db2"));
            _loggedMentor = new User();
            
        }


        public IActionResult Index()
        {
            _loggedMentor.Id = (int)TempData["loggedUserId"];
            return View();
        }


        [HttpGet]
        public IActionResult Students()
        {
            var studentsAndClasses = _mentorOperationsFromDB.GetStudentsByMentorAndClassId(_loggedMentor.Id, 0);
            return View(studentsAndClasses);

        }
        [HttpPost]
        public IActionResult Students(int classId)
        {
            if (classId == 0)
            {
                return RedirectToAction("Students");
            }
            var studentsAndClasses = _mentorOperationsFromDB.GetStudentsByMentorAndClassId(_loggedMentor.Id, classId);
            return View(studentsAndClasses);
        }

        [HttpGet]
        public IActionResult AddStudent()
        {
            ViewModelStudentClasses studentAndClasses = new ViewModelStudentClasses();
            studentAndClasses.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentor.Id);
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
            ViewModelAddTeam viewModelAddTeam = new ViewModelAddTeam();
            viewModelAddTeam.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentor.Id);
            return View(viewModelAddTeam);
        }

        [HttpPost]
        public IActionResult AddTeam(int classId)
        {
            ViewModelAddTeam viewModelAddTeam = new ViewModelAddTeam();
            viewModelAddTeam.Classes = _mentorOperationsFromDB.GetClassesByMentorId(_loggedMentor.Id);
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
    }
}
