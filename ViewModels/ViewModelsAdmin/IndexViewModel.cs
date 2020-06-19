using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsAdmin
{
    public class IndexViewModel
    {
        public User User { get; set; }
        public Class Class { get; set; }
        public ExpLevel Level { get; set; }

        public IndexViewModel()
        {
            User = new User();
            Class = new Class();
            Level = new ExpLevel();
        }
    }
}