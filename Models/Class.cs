using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }

        public Class() { }

        public Class(int id, string name, string city)
        {
            Id = id;
            Name = name;
            City = city;
        }
    }
}
