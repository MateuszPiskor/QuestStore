using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Services
{
    public class DataBaseConnection
    {
        public string HostAddress { get; set; }
        public string HostName { get; set; }
        public string HostPassword { get; set; }
        public string DatabaseName { get; set; }

        public DataBaseConnection(string hostAddress, string hostName, string hostPassword, string databaseName)
        {
            HostAddress = hostAddress;
            HostName = hostName;
            HostPassword = hostPassword;
            DatabaseName = databaseName;
        }
    }
}
