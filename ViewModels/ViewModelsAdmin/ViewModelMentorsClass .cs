using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class ViewModelMentorsClass
    {
        public List<User> Mentors { get; set; }
        public Class Class { get; set; }
        public int MentorId { get; set; }
        public List<String> Cities { get; set; }

        public ViewModelMentorsClass()
        {
            Mentors = new List<User>();
            Class = new Class();
        }
    }
}