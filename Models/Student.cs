using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class Student:User
    {
        new public int Id { get; set; }
        public int Class_id { get; set; }
        public int Team_id_coolcoins { get; set; }
        public int Exp_level_id { get; set; }

    }
}
