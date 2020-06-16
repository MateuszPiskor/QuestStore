using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelMarkQuest
    {
        public Student Student { get; set; }
        public string QuestType { get; set; }
        public List<string> QuestTypes { get; set; }
        public List<Quest> Quests { get; set; }

        public ViewModelMarkQuest()
        {
            Student = new Student();
            QuestTypes = new List<string>();
            Quests = new List<Quest>();
        }
    }
}
