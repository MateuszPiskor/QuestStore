using System.Collections.Generic;
using Queststore.Models;
namespace Queststore.DAO
{
    public interface IStudent
    {
        List<Student> GetStudentClassMembers(int studentId);
        List<Student> GetStudentTeamMembers(int studentId);
        Student GetStudentById(int studentId);
        void AddArtifacts(List<Artifact> artifacts, int studentId);
        void AddQuests(List<Quest> quests, int studentId);
        void UpdateCoolcoins(int studentId, int coolcoin, IMentor mentorDao);
        void UpdateExperienceLevel(int studentId, ExpLevel expLevel);
        int  GetCoolcoinsByStudentId(int studentId);
        public ExpLevel GetStudentExpLevel(int studentId);
        List<Artifact> GetArtifactsByStudentId(int student);
    }
}
