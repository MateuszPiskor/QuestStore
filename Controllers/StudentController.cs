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
        private readonly IStudent _studentSqlDao;
        private int _loggedStudent => Convert.ToInt32(HttpContext.Session.GetString("activeUserId"));

        public StudentController()
        {
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
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var student = _studentSqlDao.GetStudentById(studentId);
            var wallet = new Wallet()
            {
                StudentName = student.Name + " " + student.Surname,
                Artifacts = _studentSqlDao.GetArtifactsByStudentId(studentId),
                Coolcoins = student.Coolcoins,
                ExperienceLevel = student.ExpLevel
            };
            return View(wallet);
        }

        [HttpGet]
        public IActionResult Shop()
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var student = _studentSqlDao.GetStudentById(studentId);
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
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var classStudents = _studentSqlDao.GetStudentClassMembers(studentId);
            return View(classStudents);
        }

        public IActionResult MyTeam()
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var team = _studentSqlDao.GetStudentTeamMembers(studentId);
            return View(team);
        }

        [HttpGet]
        public IActionResult BuyBasicItem(int artifactId)
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var student = _studentSqlDao.GetStudentById(studentId);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(artifactId);
            var buyBasicItem = new BuyBasicArtifact(student, artifact);
      
            return View(buyBasicItem);
        }

        [HttpPost]
        public IActionResult BuyBasicItem(BuyBasicArtifact buyBasicArtifact)
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var student = _studentSqlDao.GetStudentById(studentId);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(buyBasicArtifact.BasicArtifact.Id);
            _studentSqlDao.AddArtifact(artifact, buyBasicArtifact.LoggedStudent.Id);
            _studentSqlDao.UpdateCoolcoins(buyBasicArtifact.LoggedStudent.Id, (student.Coolcoins - artifact.Price));
            return RedirectToAction("Shop", "Student");
        }

        [HttpGet]
        public IActionResult BuyMagicItem(int artifactId)
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var student = _studentSqlDao.GetStudentById(studentId);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(artifactId);
            var team = _studentSqlDao.GetStudentTeamMembers(studentId);
            var buyMagicItem = new BuyMagicArtifact
            {
                OkToBuyArtifact = true,
                MagicArtifact = artifact,
                Student = student
            };

            if (team.Count == 0)
                return View(buyMagicItem);
            else
                buyMagicItem.IsAssignedToTeam = true;
            
            var pricePerStudent = artifact.Price / team.Count; ;
            foreach(var teamPerson in team)
            {
                if (teamPerson.Coolcoins < pricePerStudent)
                {
                    buyMagicItem.OkToBuyArtifact = false;
                    break;
                }
            }
            return View(buyMagicItem);
        }

        [HttpPost]
        public IActionResult BuyMagicItem(BuyMagicArtifact buyMagicArtifact)
        {
            var studentId = _studentSqlDao.GetStudentIdByUserId(_loggedStudent);
            var team = _studentSqlDao.GetStudentTeamMembers(studentId);
            var artifact = _studentSqlDao.GetArtifactByArtifactId(buyMagicArtifact.MagicArtifact.Id);
            foreach(var teamMember in team)
            {
                _studentSqlDao.AddArtifact(artifact, teamMember.Id);
                _studentSqlDao.UpdateCoolcoins(teamMember.Id, teamMember.Coolcoins - artifact.Price / team.Count);
            }

            return RedirectToAction("Shop", "Student");
        }
    }
}
