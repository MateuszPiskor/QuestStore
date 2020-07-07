using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsStudent
{
    public class Shop
    {
        public Student LoggedStudent { get; set; }
        public List<Artifact> BasicArtifacts { get; set; }
        public List<Artifact> MagicArtifacts { get; set; }

    }
}

