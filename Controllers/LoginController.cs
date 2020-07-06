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
                    if (user.IsAdmin == true && user.IsMentor == false)
                    {
                        Response.Cookies.Append("UserRole", "Admin");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.IsMentor == true && user.IsAdmin == false)
                    {
                        _sessionManager.LoggedUserRole = "Mentor";
                        return RedirectToAction("Index", "Mentor");
                    }
                    else if (user.IsMentor == true && user.IsAdmin == true)
                    {
                        Response.Cookies.Append("UserRole", "AdminMentor");
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.IsStudent == true)
                    {
                        Response.Cookies.Append("UserRole", "Student");
                        return RedirectToAction("Index", "Student");
                    }
                }
            }
            return View();
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
