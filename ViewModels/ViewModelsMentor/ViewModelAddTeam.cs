using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelAddTeam
    {
        public int ClassId { get; set; }
        public List<Student> Students { get; set; }
        public List<Student> SelectedStudents { get; set; }
        public List<Class> Classes { get; set; }
        public string TeamName { get; set; }

        public ViewModelAddTeam()
        {
            Students = new List<Student>();
            SelectedStudents = new List<Student>();
            Classes = new List<Class>();
        }
    }
}
