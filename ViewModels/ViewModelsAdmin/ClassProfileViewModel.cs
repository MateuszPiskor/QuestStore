using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class ClassProfileViewModel
    {
       
        public Class Class { get; set; }
        public List<User> Mentors { get; set; }

        public ClassProfileViewModel()
        {
            Mentors = new List<User>();
            Class = new Class();
        }
    }
}
