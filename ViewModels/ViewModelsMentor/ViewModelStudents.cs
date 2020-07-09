using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelStudents
    {
        public Student Student { get; set; }
        public List<Student> Students { get; set; }
        public List<Class> Classes { get; set; }
        public int ClassId { get; set; }

        public ViewModelStudents()
        {
            Student = new Student();
            Classes = new List<Class>();
        }
    }
}
