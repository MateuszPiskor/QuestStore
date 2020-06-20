using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Queststore.Models;

namespace Queststore.DAO
{
    public class DataStorageOperationsFromJson : IDataStorage
    {
        private readonly string _fileName;

        public DataStorageOperationsFromJson(string fileName)
        {
            _fileName = fileName;
        }

        public string GetPassword(string email)
        {
            string password = "";
            var jsonString = File.ReadAllText(_fileName);
            var logins = JsonSerializer.Deserialize<List<Login>>(jsonString);
            foreach (var item in logins)
            {
                if (item.Email == email)
                {
                    password = item.Password;
                }  
            }
            return password;
        }
    }
}
