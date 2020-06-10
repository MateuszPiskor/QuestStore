;using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Queststore.Services;
using ShopOnline.DataAccess;

namespace Queststore.DAO
{
    public class AdminOperations : IAdmin
    {
        private readonly DataBaseConnectionService _dataBaseConnectionService;
        public AdminOperations(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress, dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
        }
        public void AddLevelForm()
        {
            
        }
    }
}
