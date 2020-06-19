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
        public StudentOperationsFromDB(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress,
                                            dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
            _dataBaseConnection = dataBaseConnection;
        }

        public DataBaseConnectionService DataBaseConnection { get; }

        public List<Student> GetStudentsByClassId(int classId, IMentor mentorDao)
        {
            return mentorDao.GetStudentsByClassId(classId);
        }

        public Student GetStudentById(int studentId, IMentor mentorDao)
        {
            return mentorDao.GetStudentById(studentId);
        }

        public void UpdateCoolcoins(int studentId, int coolcoin, IMentor mentorDao)
        {
            mentorDao.UpdateStudentCoolcoins(studentId, coolcoin);
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
                collcoins = reader.GetInt32(3);
            }
            return collcoins;

        }

        public List<Artifact> GetArtifactsByStudentId(int studentId)
        {
            List<Artifact> artifacts = new List<Artifact>();
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"SELECT artifacts.id, artifacts.name, artifacts.description,
                            artifacts.price, artifacts.type FROM artifacts
                            JOIN student_artifact
                            ON student_artifact.artifact_id = artifacts.id
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
        private void AddArtifact(Artifact artifact, int studentId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"INSERT INTO student_artifact(student_id, artifact_id)
                            VALUES(@studentId, @artifactId);";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("studentId", studentId);
            cmd.Parameters.AddWithValue("artifactId", artifact.Id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }
        private void AddQuest(Quest quest, int studentId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = $@"INSERT INTO student_quest(student_id, quest_id)
                            VALUES(@studentId, @questId);";
            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();
            cmd.Parameters.AddWithValue("studentId", studentId);
            cmd.Parameters.AddWithValue("questId", quest.Id);

            cmd.Prepare();
            cmd.ExecuteNonQuery();

        }

    }
}