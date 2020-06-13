using System;
using System.Collections.Generic;
using Queststore.Models;

namespace Queststore.ViewModels.ViewModelsMentor
{
    public class ViewModelQuests
    {
        public List<Quest> Quests { get; set; }
        public string QuestType { get; set; }
        public List<string> QuestTypes { get; set; }

        public ViewModelQuests()
        {
        }
    }
}
