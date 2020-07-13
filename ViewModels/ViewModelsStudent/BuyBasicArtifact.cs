using System;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsStudent
{
    public class BuyBasicArtifact
    {
        public Student LoggedStudent { get; set; }
        public Artifact BasicArtifact { get; set; }
        public BuyBasicArtifact(Student student, Artifact artifact)
        {
            LoggedStudent = student;
            BasicArtifact = artifact;
        }
        public BuyBasicArtifact()
        {

        }
    }
}
