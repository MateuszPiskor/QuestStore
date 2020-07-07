using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
        private int _loggedStudent => Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));

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
            var student = _studentSqlDao.GetStudentById(_loggedStudent);
            var basicArtifacts = _studentSqlDao.GetArtifactsByType("basic");
            var magicArtifacts = _studentSqlDao.GetArtifactsByType("magic");
            var shop = new Shop()
            {
                LoggedStudent = student,
                BasicArtifacts = basicArtifacts,
                MagicArtifacts = magicArtifacts
            };
            return View(shop);
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

        [HttpGet]
        public IActionResult BuyBasicItem(int artifactId)
        {
            var student = _studentSqlDao.GetStudentById(_loggedStudent);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(artifactId);
            var buyBasicItem = new BuyBasicArtifact(student, artifact);
      
            return View(buyBasicItem);
        }

        [HttpPost]
        public IActionResult BuyBasicItem([FromForm] BuyBasicArtifact buyBasicArtifact)
        {
            var student = _studentSqlDao.GetStudentById(buyBasicArtifact.LoggedStudent.Id);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(buyBasicArtifact.BasicArtifact.Id);
            _studentSqlDao.AddArtifact(artifact, buyBasicArtifact.LoggedStudent.Id);
            _studentSqlDao.UpdateCoolcoins(buyBasicArtifact.LoggedStudent.Id, (student.Coolcoins - artifact.Price));
            return RedirectToAction("Shop", "Student");
        }
    }
}
