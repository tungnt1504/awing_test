namespace awing_test.Server.Models.DTOs
{
    public class SoaTemplateDTO
    {
        public string SoaCode { get; set; }
        public string SoaMessage { get; set; }
        public SoaTemplateDTO(string code, string message) 
        { 
            this.SoaCode = code;
            this.SoaMessage = message;
        }
    }
}