using awing_test.Server.Handles;
using awing_test.Server.Helpers;
using awing_test.Server.Mappers;
using awing_test.Server.Models.DTOs;
using awing_test.Server.Models.DTOs.Base;
using Microsoft.AspNetCore.Mvc;

namespace awing_test.Server.Controllers
{
    [ApiController]
    [Route("calculate")]
    public class CalculateController : ControllerBase
    {
        public ResponseHelper responseHelper = new ResponseHelper();
        public RequestValidationHelper requestValidationHelper = new RequestValidationHelper();
        public HandleCalculate handleCalculate = new HandleCalculate();
        public CalculateMapper calculateMapper = new CalculateMapper();

        public CalculateService sqlite = new CalculateService("demo.db");
        

        /// <summary>
        /// API thực hiện tính toán
        /// </summary>
        /// <param name="calculateRequest"></param>
        /// <returns></returns>
        [HttpPost("do-calculate")]
        public async Task<IActionResult> DoCalculateAsync(CalculateRequestDTO calculateRequest)
        {
            try
            {
                float output = 0;


                (bool isError, string message) = requestValidationHelper.CalculateRequestValidate(calculateRequest);
                // validate đầu vào
                if (isError)
                {
                    return BadRequest(responseHelper.ValidationResponse(message));
                }

                // thực hiện tính toán
                double outputValue = handleCalculate.DoCalculate(calculateRequest);

                

                return Ok(responseHelper.SuccessResponse(new CalculateResponseDTO() { Output = outputValue }));
            } catch (Exception e)
            {
                return StatusCode(500, responseHelper.ErrorResponse(e.Message));
            }
            
        }

        /// <summary>
        /// API lưu lại lịch sử sau khi tính toán
        /// </summary>
        /// <param name="calculateRequest"></param>
        /// <returns></returns>
        [HttpPost("save-history")]
        public async Task<IActionResult> GetAllRecords(CalculateRequestDTO calculateRequest)
        {
            try
            {
                await sqlite.InsertRecordAsync(calculateMapper.RequestDTOToEntity(calculateRequest));
                return Ok(responseHelper.SuccessResponse(null));
            }
            catch (Exception e)
            {
                return StatusCode(500, responseHelper.ErrorResponse(e.Message));
            }
        }

        /// <summary>
        /// API lấy danh sách lịch sử
        /// </summary>
        /// <returns></returns>
        [HttpGet("get-history")]
        public async Task<IActionResult> GetAllRecords()
        {
            try
            {
                var allRecords = await sqlite.GetAllRecordsAsync();
                var output = calculateMapper.EntityToListDTO(allRecords);
                return Ok(responseHelper.SuccessResponse(new CalculateResponseDTO() { Output = output }));
            }
            catch (Exception e)
            {
                return StatusCode(500, responseHelper.ErrorResponse(e.Message));
            }
        }
    }
}
