using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Queststore.Models
{
    public class ExpLevel
    {
        public int Id { get; set; }
        [Required(ErrorMessage ="You have to type a name")]
        
        public string Name { get; set; }
        [Required(ErrorMessage = "You have to set number of points")]
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
