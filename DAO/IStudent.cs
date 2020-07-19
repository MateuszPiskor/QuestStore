using System.Collections.Generic;
using Queststore.Models;
namespace Queststore.DAO
{
    public interface IStudent
    {
        List<Student> GetStudentClassMembers(int studentId);
        List<Student> GetStudentTeamMembers(int studentId);
        int GetStudentIdByUserId(int userId);
        Student GetStudentById(int studentId);
        void AddArtifact(Artifact artifacts, int studentId);
        void AddQuest(Quest quests, int studentId);
        void UpdateExperienceLevel(int studentId, ExpLevel expLevel);
        int  GetCoolcoinsByStudentId(int studentId);
        public ExpLevel GetStudentExpLevel(int studentId);
        List<Artifact> GetArtifactsByStudentId(int student);
        List<Artifact> GetArtifactsByType(string type);
        Artifact GetArtifactByArtifactId(int artifactId);
        void UpdateCoolcoins(int studentId, int coolcoins);
    }
}
