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
            string command = $@"INSERT INTO exp_levels(name,min_points)
                       VALUES('{expLevel.Name}','{expLevel.MinPoints}')";
            ExecuteNonQueryCommand(command);
        }
        public void EditExpierenceLevelForm(ExpLevel expLevel)
        {

        }

        public List<ExpLevel> ExpLevelsList()
        {

            string command= $@"Select * from exp_levels";
            return ExecuteCommand(command);
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

        private List<ExpLevel> ExecuteCommand(string command)
        {
            List<ExpLevel> levels = new List<ExpLevel>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                    levels = ExpLevMaker.ParseDbTo(levels,rdr);
                con.Close();
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Jebło coś innego: {e.Message}");
                throw;
            }
            return levels;
        }
    }
}
