﻿using System;
using System.Collections.Generic;
using System.Linq;
using Npgsql;
using Queststore.Models;
using Queststore.Services;
using Queststore.ViewModels.ViewModelsMentor;

namespace Queststore.DAO
{
    public class MentorOperationsFromDB : IMentor
    {
        private readonly DataBaseConnectionService _dataBaseConnectionService;

        public MentorOperationsFromDB(DataBaseConnection dataBaseConnection)
        {
            _dataBaseConnectionService = new DataBaseConnectionService(dataBaseConnection.HostAddress, dataBaseConnection.HostName, dataBaseConnection.HostPassword, dataBaseConnection.DatabaseName);
        }

        public List<Class> GetClassesByMentorId(int mentorId)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT classes.id, classes.name
                            FROM classes
                            LEFT JOIN mentor_class
                            ON classes.id = mentor_class.class_id
                            WHERE mentor_class.user_id = '{mentorId}';";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            List<Class> classes = new List<Class>();

            while (reader.Read())
            {
                Class @class = new Class();
                @class.Id = reader.GetInt32(0);
                @class.Name = reader.GetString(1);
                classes.Add(@class);
            }

            return classes;
        }

        public List<Student> GetStudentsByClassId(int classId)
        {
            List<Student> students = new List<Student>();

            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @$"SELECT users.id, users.name, users.surname, students.language, classes.id, classes.name
                            FROM users
                            INNER JOIN students
                            ON users.student_id = students.id
                            LEFT JOIN classes
                            ON students.class_id = classes.id
                            WHERE classes.id = {classId};";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);
            using NpgsqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                Class @class = new Class();
                @class.Id = reader.GetInt32(4);
                @class.Name = reader.GetString(5);

                Student student = new Student();
                student.Id = reader.GetInt32(0);
                student.Name = reader.GetString(1);
                student.Surname = reader.GetString(2);
                student.Language = reader.GetString(3);
                student.Class = @class;
                students.Add(student);

            }
            return students;
        }

        public ViewModelStudentsClasses GetStudentsByMentorAndClassId(int mentorId, int classId)
        {
            ViewModelStudentsClasses viewModelStudentsClasses = new ViewModelStudentsClasses();
            viewModelStudentsClasses.Classes = GetClassesByMentorId(mentorId);
            viewModelStudentsClasses.Students = GetStudentsByClassId(classId);
 
            return viewModelStudentsClasses;
        }

        public void AddQuest(Quest quest)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"INSERT INTO quests(name, description, value, type)
                        VALUES (@name, @description, @value, @type)";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("name", quest.Name);
            cmd.Parameters.AddWithValue("description", quest.Description);
            cmd.Parameters.AddWithValue("value", quest.Value);
            cmd.Parameters.AddWithValue("type", quest.Type);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }

        public void AddArtifact(Artifact artifact)
        {
            using var con = _dataBaseConnectionService.GetDatabaseConnectionObject();
            string sql = @"INSERT INTO artifacts(name, description, price, type)
                        VALUES (@name, @description, @price, @type)";

            con.Open();
            using var cmd = new NpgsqlCommand(sql, con);

            cmd.Parameters.AddWithValue("name", artifact.Name);
            cmd.Parameters.AddWithValue("description", artifact.Description);
            cmd.Parameters.AddWithValue("price", artifact.Price);
            cmd.Parameters.AddWithValue("type", artifact.Type);

            cmd.Prepare();
            cmd.ExecuteNonQuery();
        }
    }
}
