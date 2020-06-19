using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.DAO
{
    public interface IAdmin
    {
        void AddClass(Class group);

        void AddClassesMentor(List<int> classesIds, int id);

        void AddClassMentor(int classId, int mentorId);

        void AddClassMentors(List<int> userId, int classId);

        void AddLevelForm(ExpLevel expLevel);

        void AddMentor(User mentor);

        void AddMentorClasses(List<int> classesIds, int id);

        void EditClass(Class group);

        void EditExpierenceLevelForm(ExpLevel expLevel);

        void EditMentor(int id, User mentor);

        void EditMentor(User mentor);

        IEnumerable<ExpLevel> ExpLevelsList();

        List<string> GetCities();

        Class getClassByClassId(int id);

        public List<Class> GetClasses();

        List<Class> GetClassesByMentorId(int id);

        List<Class> GetClassesByUserId(int id);

        ExpLevel GetLevelById(int id);

        int GetMaxClassId();

        int GetMaxMentorId();

        List<User> GetMentors();

        List<User> GetMentorsByClassId(int id);

        User GetUserById(int id);

        void RemoveAllClassesToCurrentMentor(int id);

        void RemoveAllMentorsFromCurrentClass(int classId);
    }
}