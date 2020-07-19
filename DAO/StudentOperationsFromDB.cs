using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsMentor;

namespace Queststore.DAO
{
    public class StudentOperationsFromDB : IStudent
    {
        private readonly DataBaseConnection _dataBaseConnection;
        private readonly DataBaseConnectionService _dataBaseConnectionService;
        private readonly string _fullStudentQuery = @$"SELECT users.name, users.surname,
                                                        students.language, students.class_id, classes.name,
                                                        students.team_id, teams.name,
                                                        students.exp_level_id, exp_levels.name, exp_levels.min_points,
                                                        students.id, students.coolcoins
                                                    FROM users
                                                    JOIN students ON users.student_id = students.id
                                                    LEFT JOIN classes ON students.class_id = classes.id
                                                    LEFT JOIN teams ON students.team_id = teams.id
                                                    LEFT JOIN exp_levels ON students.exp_level_id = exp_levels.id
                                                    WHERE ";

        public StudentOperationsFromDB(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress,
                                            dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
            _dataBaseConnection = dataBaseConnection;
        }

        public DataBaseConnectionService DataBaseConnection { get; }

        public Student GetStudentById(int studentId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = _fullStudentQuery + $"students.id = {studentId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            var student = new Student();

            while (reader.Read())
            {
                student = ParseDBToStudent(reader);
            }

            return student;
        }

        public void UpdateCoolcoins(int studentId, int coolcoins)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"UPDATE students
                            SET coolcoins={coolcoins}
                            WHERE id={studentId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public void UpdateExperienceLevel(int studentId, ExpLevel expLevel)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"UPDATE students SET exp_level_id = {expLevel.Id}
                            WHERE id = {studentId};";
        }

        public int GetCoolcoinsByStudentId(int studentId)
        {
            int collcoins = 0;
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT coolcoins FROM students WHERE students.id = {studentId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                collcoins = reader.GetInt32(0);
            }
            return collcoins;

        }

        public List<Artifact> GetArtifactsByStudentId(int studentId)
        {
            List<Artifact> artifacts = new List<Artifact>();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT artifacts.id, artifacts.name, artifacts.description,
                            artifacts.price, artifacts.type FROM artifacts
                            JOIN student_artifact ON student_artifact.artifact_id = artifacts.id
                            WHERE student_artifact.student_id = {studentId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var artifact = new Artifact();
                artifact.Id = reader.GetInt32(0);
                artifact.Name = reader.GetString(1);
                artifact.Description = reader.GetString(2);
                artifact.Price = reader.GetInt32(3);
                artifact.Type = reader.GetString(4);
                artifacts.Add(artifact);
            }

            return artifacts;
        }
        public void AddArtifacts(List<Artifact> artifacts, int studentId)
        {
            foreach (var artifact in artifacts)
                AddArtifact(artifact, studentId);
        }

        public void AddQuests(List<Quest> quests, int studentId)
        {
            foreach (var quest in quests)
                AddQuest(quest, studentId);
            
        }
        public void AddArtifact(Artifact artifact, int studentId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"INSERT INTO student_artifact(student_id, artifact_id)
                            VALUES(@studentId, @artifactId);";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("studentId", studentId);
            cmd.Parameters.AddWithValue("artifactId", artifact.Id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        public void AddQuest(Quest quest, int studentId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"INSERT INTO student_quest(student_id, quest_id)
                            VALUES(@studentId, @questId);";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            cmd.Parameters.AddWithValue("studentId", studentId);
            cmd.Parameters.AddWithValue("questId", quest.Id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

        public List<Student> GetStudentClassMembers(int studentId)
        {
            var classId = GetClassId(studentId);
            List<Student> students = new List<Student>();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = _fullStudentQuery + $"classes.id = {classId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(ParseDBToStudent(reader));
            }
            return students;
        }

        public List<Student> GetStudentTeamMembers(int studentId)
        {
            var teamId = GetTeamId(studentId);
            List<Student> students = new List<Student>();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = _fullStudentQuery + $"teams.id = {teamId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                students.Add(ParseDBToStudent(reader));
            }
            return students;

        }

        public ExpLevel GetStudentExpLevel(int studentId)
        {
            var expLevel = new ExpLevel();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT exp_levels.id, exp_levels.name, exp_levels.min_points FROM exp_levels
                            JOIN students ON exp_level_id = exp_levels.id
                            WHERE students.id = {studentId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                expLevel.Id = reader.GetInt32(0);
                expLevel.Name = reader.GetString(1);
                expLevel.MinPoints = reader.GetInt32(2);
            }

            return expLevel;
        }

        private Student ParseDBToStudent(NpgsqlDataReader reader)
        {
            var myClass = new Class()
            {
                Id = reader.GetInt32(3),
                Name = reader.GetString(4)
            };

            var myTeam = new Team()
            {
                Id = reader.GetInt32(5),
                Name = reader.GetString(6)
            };

            var expLevel = new ExpLevel()
            {
                Id = reader.GetInt32(7),
                Name = reader.GetString(8),
                MinPoints = reader.GetInt32(9)
            };

            var student = new Student()
            {
                Id = reader.GetInt32(10),
                Name = reader.GetString(0),
                Surname = reader.GetString(1),
                Language = reader.GetString(2),
                Coolcoins = reader.GetInt32(11),
                Class = myClass,
                Team = myTeam,
                ExpLevel = expLevel
            };

            return student;
        }

        private int GetTeamId(int studentId)
        {
            int teamId = 0;
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT team_id FROM students WHERE students.id = {studentId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                teamId = reader.GetInt32(0);
            }
            return teamId;
        }

        private int GetClassId(int studentId)
        {
            int classId = 0;
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT class_id FROM students WHERE students.id = {studentId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                classId = reader.GetInt32(0);
            }
            return classId;
        }

        public List<Artifact> GetArtifactsByType(string type)
        {
            List<Artifact> artifacts = new List<Artifact>();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT id, name, description, price FROM artifacts WHERE type = '{type}';";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                var artifact = new Artifact();
                artifact.Id = reader.GetInt32(0);
                artifact.Name = reader.GetString(1);
                artifact.Description = reader.GetString(2);
                artifact.Price = reader.GetInt32(3);
                artifacts.Add(artifact);
            }

            return artifacts;
        }

        public Artifact GetArtifactByArtifactId(int artifactId)
        {
            var artifact = new Artifact();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT name, description, price, type FROM artifacts WHERE id = {artifactId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                artifact.Id = artifactId;
                artifact.Name = reader.GetString(0);
                artifact.Description = reader.GetString(1);
                artifact.Price = reader.GetInt32(2);
                artifact.Type = reader.GetString(3);
            }
            return artifact;
        }

        public int GetStudentIdByUserId(int userId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT student_id FROM users WHERE id = {userId};";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            return reader.Read() ? reader.GetInt32(0) : 0;
        }
    }
}