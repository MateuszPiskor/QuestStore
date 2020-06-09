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
        public string Address { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public int MyProperty { get; set; }
        public bool Is_Admin { get; set; }
        public bool Is_Mentor { get; set; }
        public int Student_id { get; set; }
    }
}
