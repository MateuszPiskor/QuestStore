using System.Collections.Generic;
using Queststore.Models;
namespace Queststore.DAO
{
    public interface IStudent
    {
        List<Student> GetStudentsByClassId(int classId, IMentor mentorDao);
        Student GetStudentById(int studentId, IMentor mentorDao);
        void AddArtifacts(List<Artifact> artifacts, int studentId);
        void AddQuests(List<Quest> quests, int studentId);
        void UpdateCoolcoins(int studentId, int coolcoin, IMentor mentorDao);
        void UpdateExperienceLevel(int studentId, ExpLevel expLevel);
        int  GetCoolcoinsByStudentId(int studentId);
        List<Artifact> GetArtifactsByStudentId(int student);
    }
}
