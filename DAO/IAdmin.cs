using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Queststore.Models;

namespace Queststore.DAO
{
    public interface IAdmin
    {
        void AddLevelForm(ExpLevel expLevel);
        void EditExpierenceLevelForm(ExpLevel expLevel);
        IEnumerable<ExpLevel> ExpLevelsList();
        ExpLevel GetLevelById(int id);
        void AddClass(Class group);
        public List<Class> GetClasses();
    }
}