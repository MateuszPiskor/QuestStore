using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class ExpLevel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int MinPoints { get; set; }
        public DateTime CreateTime { get; set; }

        public ExpLevel()
        {
        }

        public ExpLevel(int id, string name, int minPoints)
        {
            Id = id;
            Name = name;
            MinPoints = minPoints;
        }

    }
}
