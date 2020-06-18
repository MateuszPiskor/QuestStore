using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class EditClassFormViewModel
    {
        public List<User> Mentors { get; set; }
        public List<User> MentorsPreviousChecked { get; set; }
        public Class Class { get; set; }
        public int MentorId { get; set; }
        public List<String> Cities { get; set; }

        public EditClassFormViewModel()
        {
            Mentors = new List<User>();
            Class = new Class();
        }
    }
}
