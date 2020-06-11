using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Npgsql;
using Queststore.Models;

namespace Queststore.Services
{
    public static class ExpLevMaker
    {
        public static List<ExpLevel> ParseDbTo(this List<ExpLevel> levels, NpgsqlDataReader rdr)
        {
            levels.Add(new ExpLevel(rdr.GetInt32(0), rdr.GetString(1), rdr.GetInt32(2)));
            return levels;
        }
    }
}
