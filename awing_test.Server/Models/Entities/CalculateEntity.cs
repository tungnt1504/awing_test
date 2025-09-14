namespace awing_test.Server.Models.Entities
{
    public class CalculateEntity
    {
        public int Id { get; set; } // Auto increment primary key
        public int N { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public string Matrix { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
