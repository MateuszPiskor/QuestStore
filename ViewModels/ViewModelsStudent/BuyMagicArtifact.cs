using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsStudent
{
    public class BuyMagicArtifact
    {
        public Student Student { get; set; }
        public Artifact MagicArtifact { get; set; }
        public bool OkToBuyArtifact { get; set; }
        public bool IsAssignedToTeam { get; set; }

    }
}
