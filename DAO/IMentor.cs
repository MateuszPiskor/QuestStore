using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Queststore.Models;
using Queststore.ViewModels.ViewModelsMentor;

namespace Queststore.DAO
{
    public interface IMentor
    {
        List<Student> GetStudentsByClassId(int classId);
        List<Class> GetClassesByMentorId(int mentorId);
        ViewModelStudentsClasses GetStudentsByMentorAndClassId(int mentorId, int classId);
        void AddQuest(Quest quest);
        void AddArtifact(Artifact artifact);
        void AddStudent(Student student, int classId);
        void AddUser(Student newStudent);
        int GetMaxStudentId();
    }
}