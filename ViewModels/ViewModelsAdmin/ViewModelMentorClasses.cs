using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class ViewModelMentorClasses
    {
        public User Mentor { get; set; }
        public List<Class> Classes { get; set; }
        public int ClassId { get; set; }

        public ViewModelMentorClasses()
        {
            Mentor = new User();
            Classes = new List<Class>();
        }
    }
}