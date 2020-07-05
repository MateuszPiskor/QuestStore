using System;
using Npgsql;
using Queststore.Models;
using Queststore.Services;

namespace Queststore.DAO
{
    public class LoginOperationsFromDB : ILogin
    {
        private readonly DataBaseConnectionService _dataBaseConnectionService;

        public LoginOperationsFromDB(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress, dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
        }

        public User GetUserByEmail(string email)
        {
            User user = new User();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT * FROM users WHERE email='{email}';";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                user.Id = reader.GetInt32(0);
                user.Name = reader.GetString(1);
                user.Surname = reader.GetString(2);
                user.Email = reader.GetString(3);
                user.Phone = reader.GetString(4);
                user.Address = reader.GetString(5);
                user.IsAdmin = reader.GetBoolean(8);
                user.IsMentor = reader.GetBoolean(9);
                user.IsStudent = reader.GetBoolean(11);
            }
            return user;
        }

        public int IsRegistered(string email)
        {
            int isRegistered = 0;
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT COUNT(email) FROM users WHERE email='{email}';";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                isRegistered = reader.GetInt32(0);
            }
            return isRegistered;
        }

    }
}
