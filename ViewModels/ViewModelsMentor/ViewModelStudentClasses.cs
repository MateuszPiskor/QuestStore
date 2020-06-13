using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelStudentClasses
    {
        public Student Student { get; set; }
        public List<Class> Classes { get; set; }
        public int ClassId { get; set; }

        public ViewModelStudentClasses()
        {
            Student = new Student();
            Classes = new List<Class>();
        }
    }
}
