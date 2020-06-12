using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Adrress { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMentor { get; set; }
        public Student Student { get; set; }

        public User()
        {

        }

        public User(int id, string name, string email, string phone,string adress, string login, string password, bool isAdmin, bool isMentor)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
            Adrress = adress;
            Login = login;
            Password = password;
            IsAdmin = isAdmin;
            IsMentor = isMentor;
        }

    }
}
