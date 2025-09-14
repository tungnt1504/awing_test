using System.Net;

namespace awing_test.Server.Models.DTOs.Base
{
    public class BaseResponse<T>
    {
        public HttpStatusCode Status { get; set; }     // "success" | "error"
        public string Message { get; set; }    // thông báo
        public string SoaCode { get; set; }    // mã soa code
        public string SoaMessage { get; set; } // mô tả mã soa
        public T Data { get; set; }            // dữ liệu trả về (generic)
    }
}