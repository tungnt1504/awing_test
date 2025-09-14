using awing_test.Server.Models.DTOs;

namespace awing_test.Server.Constants
{
    public static class SoaConstants
    {
        public static readonly SoaTemplateDTO RequestSuccess = new SoaTemplateDTO("COMMON_000", "Request Success");
        public static readonly SoaTemplateDTO RequestError = new SoaTemplateDTO("COMMON_001", "Request Error");
        public static readonly SoaTemplateDTO BadRequest = new SoaTemplateDTO("COMMON_002", "Bad Request");
        public static readonly SoaTemplateDTO RequestNotFound = new SoaTemplateDTO("COMMON_003", "Request Not Found");
        public static readonly SoaTemplateDTO ValidationError = new SoaTemplateDTO("COMMON_004", "Validation error");
        
    }
}