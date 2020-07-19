using System;
using Microsoft.AspNetCore.Http;
using Queststore.Models;

namespace Queststore.Services
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public int LoggedUserId
        {
            get => (GetActiveUserIdFromSession() != 0) ? GetActiveUserIdFromSession() : 0;
            set => SetLoggedUserIdInSession(value);
        }
        public string LoggedUserRole
        {
            get => (GetActiveUserRoleFromCookies() != null) ? GetActiveUserRoleFromCookies() : null;
            set => SetLoggedUserRoleInCookie(value);
        }

        public string LoggedUserName
        {
            get => (GetActiveUserNameFromCookies() != null) ? GetActiveUserNameFromCookies() : null;
            set => SetLoggedUserNameInCookie(value);
        }

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        private string GetActiveUserRoleFromCookies()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["UserRole"];
        }

        private int GetActiveUserIdFromSession()
        {
            return Convert.ToInt32(_httpContextAccessor.HttpContext.Session.GetString("activeUserId"));
        }

        private string GetActiveUserNameFromCookies()
        {
            return _httpContextAccessor.HttpContext.Request.Cookies["UserName"];

        }

        public void ClearCookies()
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Delete("UserRole");
        }

        public void ClearSession()
        {
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        private void SetLoggedUserIdInSession(int userId)
        {
            _httpContextAccessor.HttpContext.Session.SetString("activeUserId", userId.ToString());
        }

        private void SetLoggedUserRoleInCookie(string userRole)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserRole", userRole);
            
        }

        private void SetLoggedUserNameInCookie(string userName)
        {
            _httpContextAccessor.HttpContext.Response.Cookies.Append("UserName", userName);

        }
    }
}
