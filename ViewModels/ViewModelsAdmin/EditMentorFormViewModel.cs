using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class EditMentorFormViewModel
    {
        public User Mentor { get; set; }
        public List<Class> Classes { get; set; }
        public List<Class> MentorClasses { get; set; }
        public int MentorId { get; set; }

        public EditMentorFormViewModel()
        {
            Mentor = new User();
            Classes = new List<Class>();
        }
    }
}