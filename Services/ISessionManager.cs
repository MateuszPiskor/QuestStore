using System;
namespace Queststore.Services
{
    public interface ISessionManager
    {
        public int LoggedUserId { get; set; }
        public string LoggedUserRole { get; set; }

        //void SetLoggedUserIdInSession(int userId);
        //void SetLoggedUserRoleInCookie(string userRole);
        void ClearSession();
        void ClearCookies();
    }
}
