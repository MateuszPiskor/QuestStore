using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class AddClassFormViewModel
    {
        public List<User> Mentors { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public bool IsChecked { get; set; }

        public AddClassFormViewModel()
        {
            Mentors = new List<User>();
            Class = new Class();
        }
    }
}
