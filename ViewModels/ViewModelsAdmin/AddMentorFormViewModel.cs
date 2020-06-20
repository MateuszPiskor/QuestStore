using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class AddMentorFormViewModel
    {
        public User Mentor { get; set; }
        public List<Class> Classes { get; set; }
        public int ClassId { get; set; }

        public AddMentorFormViewModel()
        {
            Mentor = new User();
            Classes = new List<Class>();
        }
    }
}