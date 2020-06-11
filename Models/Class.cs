using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class Users
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public Users() { }

        public Users(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
    }
}
