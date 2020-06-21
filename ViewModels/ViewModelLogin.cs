using System;
using Queststore.Models;

namespace Queststore.ViewModels
{
    public class ViewModelLogin
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public User User { get; set; }

        public ViewModelLogin()
        {
        }
    }
}
