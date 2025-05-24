
using Microsoft.AspNetCore.Mvc;
using StockFlowService.Helpers;
using StockFlowService.Services;

namespace StockFlowService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DynamicController : ControllerBase
    {
        private readonly ProcedureService _service;

        public DynamicController(ProcedureService service)
        {
            _service = service;
        }

        [HttpPost("{procedureName}")]
        public async Task<IActionResult> CallProcedure(string procedureName, [FromBody] EncryptedRequest request)
        {
            try
            {
                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                var result = await _service.CallStoredProcedureAsync(procedureName, decrypted);
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
