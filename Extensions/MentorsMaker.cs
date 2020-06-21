using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Queststore.Models;

namespace Queststore.Services
{
    public static class MentorsMaker
    {
        public static List<User> ParseDbTo(this List<User> mentors, NpgsqlDataReader rdr)
        {
            mentors.Add(new User(rdr.GetInt32(0), rdr.GetString(1), rdr.GetString(2),rdr.GetString(3), rdr.GetString(4), rdr.GetString(5), rdr.GetString(6),rdr.GetBoolean(7), rdr.GetBoolean(8), rdr.GetBoolean(9)));
            return mentors;
        }
    }
}
