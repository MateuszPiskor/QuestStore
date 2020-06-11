namespace Queststore.Models
{
    public class Student : User
    {
        new public int Id { get; set; }
        public int Class_id { get; set; }
        public Team Team { get; set; }
        public int Coolcoins { get; set; }
        public ExpLevel ExpLevel{ get; set; }

    }
}
