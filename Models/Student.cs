namespace Queststore.Models
{
    public class Student : User
    {
        new public int Id { get; set; }
        public Class Class { get; set; }
        public Team Team { get; set; }
        public int Coolcoins { get; set; }
        public ExpLevel ExpLevel { get; set; }
        public string Language { get; set; }

        public Student(int id, string name, string surname, string email, string phone, string address, string password, bool isAdmin, bool isMentor, Class @class, Team team, int coolcoins, ExpLevel expLevel, string language) :
                        base(id, name, surname, email, phone, address, password, isAdmin, isMentor)
        {
            Id = id;
            Class = @class;
            Team = team;
            Coolcoins = coolcoins;
            ExpLevel = expLevel;
            Language = language;
        }

        public Student()
        {

        }
    }
}
