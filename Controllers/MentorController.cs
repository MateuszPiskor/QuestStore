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
            _loggedMentor = new User();
            _loggedMentor.Id = 8;
        }


        public IActionResult Index()
        {
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
    }
}
