using System.ComponentModel.DataAnnotations;

namespace awing_test.Server.Models.DTOs
{
    public class CalculateRequestDTO
    {
        public int N { get; set; }
        public int M { get; set; }
        public int P { get; set; }
        public int[][] Matrix { get; set; }
    }
}
