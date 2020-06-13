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
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsMentor { get; set; }

        public User()
        {
        }

        public User(int id, string name, string surname, string email, string phone, string address, string password, bool isAdmin, bool isMentor)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Address = address;
            Password = password;
            IsAdmin = isAdmin;
            IsMentor = isMentor;
        }

    }
}
