﻿using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Queststore.Models;
using Queststore.ViewModels.ViewModelsMentor;

namespace Queststore.DAO
{
    public interface IMentorDao
    {
        List<Student> GetStudentsByClassId(int classId);
        int GetStudentIdByUserId(int userId);
        List<Class> GetClassesByMentorId(int mentorId);
        ViewModelStudents GetStudentsByMentorAndClassId(int mentorId, int classId);
        void AddQuest(Quest quest);
        void AddArtifact(Artifact artifact);
        void AddStudent(Student student, int classId);
        void AddUser(Student newStudent);
        int GetMaxStudentId();
        List<string> GetQuestTypes();
        List<Quest> GetQuestsByType(string questType);
        Student GetStudentById(int studentId);
        void MarkQuest(int studentId, int questId);
        void UpdateStudentCoolcoins(int studentId, int coolcoins);
    }
}