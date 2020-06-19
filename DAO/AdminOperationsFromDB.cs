using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
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

            string command = $@"Select * from exp_levels";
            List<ExpLevel> expLevelsList = GetExpLevels(command);
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
                    levels = ExpLevMaker.ParseDbTo(levels, rdr);
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
            List<ExpLevel> expLevels = GetExpLevels(command);
            return expLevels[0];
        }

        public void AddClass(Class group)
        {
            string command = $@"INSERT INTO classes(name,city)
                       VALUES('{group.Name}','{group.City}')";
            ExecuteNonQueryCommand(command);
        }

        public List<Class> GetClasses()
        {
            string command = $@"SELECT * FROM Classes";
            List<Class> classes = new List<Class>();
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
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return classes;
        }

        public List<User> GetMentors()
        {
            string command = "SELECT * FROM users where is_mentor=true";
            List<User> classes = new List<User>();
            List<User> users = new List<User>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                int id = 0;
                if (rdr != null)
                {
                    while (rdr.Read())
                    {
                        User user = new User();
                        user.Id = rdr.GetInt32(0);
                        user.Name = rdr.GetString(1);
                        user.Surname = rdr.GetString(2);
                        user.Email = rdr.GetString(3);
                        user.Phone = rdr.GetString(4);
                        user.Address = rdr.GetString(5);
                        user.IsAdmin = false;
                        user.IsMentor = rdr.GetBoolean(8);
                        users.Add(user);
                    }
                    con.Close();
                }
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return users;
        }

        public void AddMentor(User mentor)
        {
            string command = $@"insert into users (name,surname,email,phone,address,is_admin,is_mentor)
             Values ('{mentor.Name}','{mentor.Surname}','{mentor.Email}',{mentor.Phone},'{mentor.Address}',{mentor.IsAdmin = false},{mentor.IsMentor = true})";
            ExecuteNonQueryCommand(command);
        }

        public int GetMaxMentorId()
        {
            int maxId = 0;
            string command = $@"SELECT max(id) from users
            where is_mentor = true";

            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();

            try
            {
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    maxId = rdr.GetInt32(0);
                }

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server - related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unkown problem occur {0}", e.Message);
            }

            return maxId;
        }

        public void AddClassMentor(int classId, int mentorId)
        {
            string command = @"insert into mentor_class(user_id, class_id)
                                    values(@user_id, @class_id)";

            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Parameters.AddWithValue("user_id", mentorId);
                cmd.Parameters.AddWithValue("class_id", classId);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown proble occur", e.Message);
            }
        }

        public User GetUserById(int id)
        {
            string command = $"select * from users where id = { id } ";
            User mentor = new User();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    mentor.Id = rdr.GetInt32(0);
                    mentor.Name = rdr.GetString(1);
                    mentor.Surname = rdr.GetString(2);
                    mentor.Email = rdr.GetString(3);
                    mentor.Phone = rdr.GetString(4);
                    mentor.Address = rdr.GetString(5);
                }

                con.Close();
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return mentor;
        }

        public List<Class> GetClassesByUserId(int id)
        {
            string command = $@"Select distinct c.id,c.name,c.city from classes c
  	                                inner join mentor_class m 	
 	                                on c.id=m.class_id	
 	                                inner join users u
  	                                on m.user_id=u.id
  	                                where u.id={id};";
            List<Class> classes = new List<Class>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    classes = ClassMaker.ParseDbTo(classes, rdr);
                }
                con.Close();
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return classes;
        }

        public void EditMentor(int id, User mentor)
        {
            string command = $@"update users
                                set name = @name,
                                surname=@surname,
                                email=@email,
                                phone=@phone,
                                address=@address
                                where id = {id}";
            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Parameters.AddWithValue("name", mentor.Name);
                cmd.Parameters.AddWithValue("surname", mentor.Surname);
                cmd.Parameters.AddWithValue("email", mentor.Email);
                cmd.Parameters.AddWithValue("phone", mentor.Phone);
                cmd.Parameters.AddWithValue("address", mentor.Address);

                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
        }

        public Class getClassByClassId(int id)
        {
            string command = $@"select * From classes
                                where id = {id}";
            Class group = new Class();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    group.Id = rdr.GetInt32(0);
                    group.Name = rdr.GetString(1);
                    group.City = rdr.GetString(2);
                }

                con.Close();
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return group;
        }

        public List<string> GetCities()
        {
            string command = "select distinct city from classes";
            List<string> cities = new List<string>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    cities.Add(rdr.GetString(0));
                }

                con.Close();
            }
            catch (PostgresException e)
            {
                System.Console.WriteLine("Server-related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
                throw;
            }
            return cities;

        }

        public void EditClass(Class group)
        {
            string command = @$"update classes
                set name = @name,
                city = @city
                where id = {group.Id}";

            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Parameters.AddWithValue("name", group.Name);
                cmd.Parameters.AddWithValue("city", group.City);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }

        }

        public void AddClassMentors(List<int> ids, int classId)
        {
            foreach (int id in ids)
            {
                string command = $@"insert into mentor_class(user_id, class_id)
                                    values({id}, {classId})
                                    ON CONFLICT
                                    DO NOTHING";

                try
                {
                    NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                    con.Open();
                    using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                }
                catch (PostgresException e)
                {
                    Console.WriteLine("Server problem occur {0}", e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown problem occur", e.Message);
                }
            }
        }

        public int GetMaxClassId()
        {
            int maxId = 0;
            string command = "SELECT max(id) from classes";

            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();

            try
            {
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = cmd.ExecuteReader();
                while (rdr.Read())
                {
                    maxId = rdr.GetInt32(0);
                }

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server - related issues occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unkown problem occur {0}", e.Message);
            }

            return maxId;
        }

        public List<User> GetMentorsByClassId(int id)
        {
            string command = $@"Select u.id,u.name, u.surname from users u
                                    inner join mentor_class m
                                    on u.id=m.user_id
                                    inner join classes c
                                    on m.class_id=c.id
                                    where c.id={id};";

            List<User> mentors = new List<User>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    User user = new User();
                    user.Id = rdr.GetInt32(0);
                    user.Name = rdr.GetString(1);
                    user.Surname = rdr.GetString(2);
                    mentors.Add(user);
                }
                con.Close();
            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
            return mentors;
        }

        public void AddClassesMentor(List<int> classesIds, int id)
        {
            foreach (int classId in classesIds)
            {
                string command = $@"insert into mentor_class(user_id, class_id)
                                    values({id}, {classId})";

                try
                {
                    NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                    con.Open();
                    using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                }
                catch (PostgresException e)
                {
                    Console.WriteLine("Server problem occur {0}", e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown problem occur", e.Message);
                }
            }
        }

        public void RemoveAllMentorsFromCurrentClass(int classId)
        {
            string command= $"delete from mentor_class where class_id = {classId} ";

            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
        }

        public List<Class> GetClassesByMentorId(int id)
        {
            string command = $@"Select c.id,c.name, c.city from classes c
                                    inner join mentor_class m
                                    on c.id=m.class_id
                                    inner join users u
                                    on m.user_id=u.id
                                    where u.id={id};";

            List<Class> classes = new List<Class>();
            using NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            try
            {
                con.Open();
                using NpgsqlCommand preparedCommand = new NpgsqlCommand(command, con);
                using NpgsqlDataReader rdr = preparedCommand.ExecuteReader();
                while (rdr.Read())
                {
                    Class @class = new Class();
                    @class.Id = rdr.GetInt32(0);
                    @class.Name = rdr.GetString(1);
                    @class.City = rdr.GetString(2);
                    classes.Add(@class);
                }
                con.Close();
            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
            return classes;
        }

        public void RemoveAllClassesToCurrentMentor(int id)
        {
            string command = $"delete from mentor_class where user_id = {id} ";

            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Prepare();
                cmd.ExecuteNonQuery();
            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
        }

        public void EditMentor(User mentor)
        {
            string command = @$"update users
                                set name = @name,
                                surname=@surname,
                                email=@email,
                                phone=@phone,
                                address=@address
                                where id = {mentor.Id}";

            try
            {
                NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                con.Open();
                using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                cmd.Parameters.AddWithValue("name", mentor.Name);
                cmd.Parameters.AddWithValue("surname", mentor.Surname);
                cmd.Parameters.AddWithValue("email", mentor.Email);
                cmd.Parameters.AddWithValue("phone", mentor.Phone);
                cmd.Parameters.AddWithValue("address", mentor.Address);
                cmd.Prepare();
                cmd.ExecuteNonQuery();

            }
            catch (PostgresException e)
            {
                Console.WriteLine("Server problem occur {0}", e.Message);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("Unknown problem occur", e.Message);
            }
        }

        public void AddMentorClasses(List<int> classesIds, int id)
        {
            foreach (int classId in classesIds)
            {
                string command = $@"insert into mentor_class(user_id, class_id)
                                    values({id}, {classId})
                                    ON CONFLICT
                                    DO NOTHING";

                try
                {
                    NpgsqlConnection con = _dataBaseConnectionService.GetDatabaseConnectionObject();
                    con.Open();
                    using NpgsqlCommand cmd = new NpgsqlCommand(command, con);
                    cmd.Prepare();
                    cmd.ExecuteNonQuery();

                }
                catch (PostgresException e)
                {
                    Console.WriteLine("Server problem occur {0}", e.Message);
                    throw;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Unknown problem occur", e.Message);
                }
            }
        }
    }
}
