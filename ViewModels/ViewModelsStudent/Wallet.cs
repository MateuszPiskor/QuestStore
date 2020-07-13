using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsStudent
{
    public class Wallet
    {
        public string StudentName { get; set; }
        public int Coolcoins { get; set; }
        public List<Artifact> Artifacts { get; set; }
        public ExpLevel ExperienceLevel { get; set; }
    }
}
