using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class AddClassFormViewModel
    {
        public List<User> Mentors { get; set; }
        public Class Class { get; set; }
        public int ClassId { get; set; }
        public List<string> Cities { get; set; } = new List<string>() { "Warszawa", "Kraków", "Wrocław" };
        public bool IsChecked { get; set; }

        public AddClassFormViewModel()
        {
            Mentors = new List<User>();
            Class = new Class();
        }
    }
}