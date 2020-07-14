using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Queststore.DAO;
using Queststore.Models;
using Queststore.Services;

namespace Queststore.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILogin _loginOperationsFromDB;
        private readonly IDataStorage _dataStorage;
        private ISessionManager _sessionManager; 

        public LoginController()
        {
            _loginOperationsFromDB = new LoginOperationsFromDB(new DataBaseConnection("localhost", "agnieszkachruszczyksilva", "startthis", "queststore"));
            //_loginOperationsFromDB = new LoginOperationsFromDB(new DataBaseConnection("localhost", "postgres", "1234", "db4"));
            //_loginOperationsFromDB = new LoginOperationsFromDB(new DataBaseConnection("localhost", "magdalenaopiola", "Lena1234", "queststore"));
            _dataStorage = new DataStorageOperationsFromJson("wwwroot/lib/data.json");
            _sessionManager = new SessionManager(new HttpContextAccessor());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            if (IsEmailRegistered(email))
            {
                if (IsPasswordCorrect(email, password))
                {
                    User user = GetUser(email);
                    _sessionManager.LoggedUserId = user.Id;
                    _sessionManager.LoggedUserName = user.Name;
                    if (IsUserAdmin(user))
                    {
                        Response.Cookies.Append("UserRole", "Admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (IsUserMentor(user))
                    {
                        _sessionManager.LoggedUserRole = "Mentor";
                        return RedirectToAction("Index", "Mentor");
                    }
                    else if (IsUserAdminAndMentor(user))
                    {
                        Response.Cookies.Append("UserRole", "AdminMentor");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (IsUserStudent(user))
                    {
                        Response.Cookies.Append("UserRole", "Student");
                        return RedirectToAction("Index", "Student");
                    }
                }
            }
            TempData["Message"] = "Login went wrong. Insert correct credentials.";
            return RedirectToAction("Index");
        }

        private static bool IsUserStudent(User user)
        {
            return user.IsStudent == true;
        }

        private static bool IsUserAdminAndMentor(User user)
        {
            return user.IsMentor == true && user.IsAdmin == true;
        }

        private static bool IsUserMentor(User user)
        {
            return user.IsMentor == true && user.IsAdmin == false;
        }

        private static bool IsUserAdmin(User user)
        {
            return user.IsAdmin == true && user.IsMentor == false;
        }

        private User GetUser(string email)
        {
            return _loginOperationsFromDB.GetUserByEmail(email);
        }

        private bool IsPasswordCorrect(string email, string password)
        {
            return _dataStorage.GetPassword(email) == password;
        }

        private bool IsEmailRegistered(string email)
        {
            return Convert.ToBoolean(_loginOperationsFromDB.IsRegistered(email));
        }

        public IActionResult Logout()
        {
            _sessionManager.ClearSession();
            _sessionManager.ClearCookies();

            return RedirectToAction("Index", "Login");
        }
    }
}
