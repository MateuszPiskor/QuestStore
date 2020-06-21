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
        private readonly ILogger _loginOperationsFromDB;
        private readonly IDataStorage _dataStorage;

        public LoginController()
        {
            _loginOperationsFromDB = new LoginOperationsFromDB(new DataBaseConnection("localhost", "agnieszkachruszczyksilva", "startthis", "queststore"));
            _dataStorage = new DataStorageOperationsFromJson("wwwroot/lib/data.json");
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
                    HttpContext.Session.SetString("activeUserId", Convert.ToString(user.Id));
                    if (user.IsAdmin == true)
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (user.IsMentor == true)
                    {
                        return RedirectToAction("Index", "Mentor");
                    }
                    else if (user.IsStudent == true)
                    {
                        return View(); // adapt to Student
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
            HttpContext.Session.Clear(); // remove all keys from session

            return RedirectToAction("Index", "Login");
        }
    }
}
