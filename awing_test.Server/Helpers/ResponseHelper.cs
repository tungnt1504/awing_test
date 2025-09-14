using awing_test.Server.Constants;
using awing_test.Server.Models.DTOs;
using awing_test.Server.Models.DTOs.Base;
using System.Net;

namespace awing_test.Server.Helpers
{
    public class ResponseHelper
    {
        public ResponseHelper() { }

        /// <summary>
        /// Body response trả về success
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public BaseResponse<object> SuccessResponse(object output)
        {
            BaseResponse<object> baseResponse = new BaseResponse<object>();
            baseResponse.Status = HttpStatusCode.OK;
            baseResponse.Message = HttpStatusCode.OK.ToString();
            baseResponse.SoaCode = SoaConstants.RequestSuccess.SoaCode;
            baseResponse.SoaMessage = SoaConstants.RequestSuccess.SoaMessage;
            baseResponse.Data = output;

            return baseResponse;
        }

        /// <summary>
        /// Body response trả về cho lỗi validate
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<object> ValidationResponse(string message)
        {
            BaseResponse<object> baseResponse = new BaseResponse<object>();
            baseResponse.Status = HttpStatusCode.BadRequest;
            baseResponse.Message = message;
            baseResponse.SoaCode = SoaConstants.ValidationError.SoaCode;
            baseResponse.SoaMessage = SoaConstants.ValidationError.SoaMessage;
            
            return baseResponse;
        }

        /// <summary>
        /// Body response trả về cho API lỗi
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public BaseResponse<object> ErrorResponse(string message)
        {
            BaseResponse<object> baseResponse = new BaseResponse<object>();
            baseResponse.Status = HttpStatusCode.BadRequest;
            baseResponse.Message = message;
            baseResponse.SoaCode = SoaConstants.RequestError.SoaCode;
            baseResponse.SoaMessage = SoaConstants.RequestError.SoaMessage;

            return baseResponse;
        }
    }
}