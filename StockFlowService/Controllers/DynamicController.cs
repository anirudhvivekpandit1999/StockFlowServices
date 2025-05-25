
using Azure.Core;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using StockFlowService.Helpers;
using StockFlowService.Services;

namespace StockFlowService.Controllers
{
    [EnableCors("AllowAll")]
    [ApiController]
    [Route("api/[controller]")]
    public class StockInAndOutInboundOutboundMovementController : ControllerBase
    {
        private readonly ProcedureService _service;

        public StockInAndOutInboundOutboundMovementController(ProcedureService service)
        {
            _service = service;
        }

        [HttpPost("AddNewForm")]
        public async Task<IActionResult> AddNewForm([FromBody] EncryptedRequest request)
        {
            try
            {
                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                Console.WriteLine("Decrypted", decrypted);
                var result = await _service.CallStoredProcedureAsync("spd_AddNewInboundForm", decrypted);
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetStockFlowData")]
        public async Task<IActionResult> GetStockFlowData()
        {
            try
            {

                //var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                var result = await _service.CallStoredProcedureAsync("spd_GetActivityCount", new Dictionary<string, object>());
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetSideBarData")]
        public async Task<IActionResult> GetSideBarData()
        {
            try
            {

                //var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                var result = await _service.CallStoredProcedureAsync("spd_GetSideBarData", new Dictionary<string, object>());
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetProductName")]
        public async Task<IActionResult> GetProductName([FromBody] EncryptedRequest request)
        {
            try
            {
                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                Console.WriteLine("Decrypted", decrypted);
                var result = await _service.CallStoredProcedureAsync("spd_GetProductNameByProductSerialNumber", decrypted);
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        public class EncryptedRequest
        {
            public string EncryptedData { get; set; }
        }
    }
}
