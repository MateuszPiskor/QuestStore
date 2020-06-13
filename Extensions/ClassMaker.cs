using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Queststore.Models;

namespace Queststore.Services
{
    public static class ClassMaker
    {
        public static List<Class> ParseDbTo(this List<Class> classes, NpgsqlDataReader rdr)
        {
            classes.Add(new Class(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2)));
            return classes;
        }
    }
}
