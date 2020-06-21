using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsStudent;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Queststore.Controllers
{
    public class StudentController : Controller
    {

        private readonly IMentor _mentorSqlDao;
        private readonly IStudent _studentSqlDao;
        private readonly int _loggedStudent = 3;

        public StudentController()
        {
            _mentorSqlDao = new MentorOperationsFromDB(new DataBaseConnection("localhost",
                                                                              "magdalenaopiola",
                                                                              "Lena1234",
                                                                              "queststore"));
            _studentSqlDao = new StudentOperationsFromDB(new DataBaseConnection("localhost",
                                                                                "magdalenaopiola",
                                                                                "Lena1234",
                                                                                "queststore"));
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Wallet()
        {
            var student = _studentSqlDao.GetStudentById(_loggedStudent);
            var wallet = new Wallet()
            {
                StudentName = student.Name + " " + student.Surname,
                Artifacts = _studentSqlDao.GetArtifactsByStudentId(_loggedStudent),
                Coolcoins = student.Coolcoins,
                ExperienceLevel = student.ExpLevel
            };
            return View(wallet);
        }

        [HttpGet]
        public IActionResult Shop()
        {
            var artifacts = new List<Artifact>();
            //var artifacts = _studentSqlDao.GetAllArtifacts();
            return View(artifacts);
        }

        [HttpGet]
        public IActionResult MyClass()
        {
            var classStudents = _studentSqlDao.GetStudentClassMembers(_loggedStudent);
            return View(classStudents);
        }

        public IActionResult MyTeam()
        {
            var team = _studentSqlDao.GetStudentTeamMembers(_loggedStudent);
            return View(team);
        }
    }
}
