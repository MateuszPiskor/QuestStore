using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Questsore.DataAccess;
using Queststore.Models;
using Queststore.Services;

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
            string command = @$"update exp_levels 
                                set name = '{expLevel.Name}',
                                min_points = {expLevel.MinPoints}
                                where id = {expLevel.Id} ";
            ExecuteNonQueryCommand(command);
        }

        public IEnumerable<ExpLevel> ExpLevelsList()
        {

            string command= $@"Select * from exp_levels";
            var expLevelsList=GetExpLevels(command);
            return sortLevelsByMinPoints(expLevelsList);
        }

        private IEnumerable<ExpLevel> sortLevelsByMinPoints(List<ExpLevel> expLevelsList)
        {
            return expLevelsList.OrderBy(level => level.MinPoints);
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

        private List<ExpLevel> GetExpLevels(string command)
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

        public ExpLevel GetLevelById(int id)
        {
            string command = $"select * from exp_levels where id = { id } ";
            var expLevels=GetExpLevels(command);
            return expLevels[0];
        }

        public void AddClass(Users group)
        {
            string command=$@"INSERT INTO classes(name,city)
                       VALUES('{group.Name}','{group.City}')";
            ExecuteNonQueryCommand(command);
        }

       

        private List<Users> GetClasses()
        {
            string command = $@"SELECT * FROM Classes";
            List<Users> classes = new List<Users>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                    classes = ClassMaker.ParseDbTo(classes, rdr);
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
            return classes;
        }

        private List<Users> GetMentors()
        {
            string command = $@"SELECT * FROM Classes";
            List<Users> mentors = new List<Users>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                    mentors = MentorsMaker.ParseDbTo(mentors, rdr);
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
            return mentors;
        }
    }
}
