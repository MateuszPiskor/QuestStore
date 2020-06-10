using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Queststore.Models;
using Queststore.Services;
using ShopOnline.DataAccess;

namespace Queststore.DAO
{
    public class AdminOperationsFromDB : IAdmin
    {
        private readonly DataBaseConnectionService _dataBaseConnectionService;
        public AdminOperationsFromDB(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress, dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
        }
        public void AddLevelForm(ExpLevel expLevel)
        {
            string command = $@"INSERT INTO exp_levels(id,name,min_points)
                       VALUES((Select max(id) from exp_levels)+1, '{expLevel.Name}','{expLevel.Min_Points}')";
            ExecuteNonQueryCommand(command);
        }
        private void ExecuteNonQueryCommand(string command)
        {

            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                preparedCommand.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server promblem {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Problem with sth else: {e.Message}");
                throw;
            }
        }
    }
}
