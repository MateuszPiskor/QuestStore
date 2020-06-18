using System;
using Queststore.Models;

namespace Queststore.DAO
{
    public interface ILogger
    {
        User GetUserByEmail(string email);
        int IsRegistered(string email);
    }
}
