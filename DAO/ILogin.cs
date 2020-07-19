using System;
using Queststore.Models;

namespace Queststore.DAO
{
    public interface ILogin
    {
        User GetUserByEmail(string email);
        int IsRegistered(string email);
    }
}
