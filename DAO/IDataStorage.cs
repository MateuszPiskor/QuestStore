using System;
namespace Queststore.DAO
{
    public interface IDataStorage
    {
        string GetPassword(string email);
    }
}
