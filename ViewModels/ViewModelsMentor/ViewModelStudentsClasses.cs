using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelStudentsClasses
    {
        public int ClassId { get; set; }
        public List<Student> Students { get; set; }
        public List<Class> Classes { get; set; }

        public ViewModelStudentsClasses()
        {
            Students = new List<Student>();
            Classes = new List<Class>();
        }
    }
}
