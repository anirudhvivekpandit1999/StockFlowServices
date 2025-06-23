using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockFlowService.Helpers;
using StockFlowService.Services;
using static StockFlowService.Controllers.StockInAndOutInboundOutboundMovementController;

namespace StockFlowService.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class NewApi : Controller
    {
        private readonly ProcedureService _service;

        public NewApi(ProcedureService service)
        {
            _service = service;
        }



        [HttpPost("GetInventoryOverview")]
        public async Task<IActionResult> GetInventoryOverview()
        {
            try
            {

                // var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("sp_GetInventoryOverview", new Dictionary<string, object>());

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetStockAnalytics")]
        public async Task<IActionResult> GetStockAnalytics()
        {
            try
            {

                // var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("sp_GetStockAnalytics", new Dictionary<string, object>());

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("insertDispatch")]
        public async Task<IActionResult> insertDispatch([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("spd_InsertDispatch", decrypted);

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("insertRecieved")]
        public async Task<IActionResult> insertRecieved([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("spd_insertRecieved", decrypted);

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("insertReturned")]
        public async Task<IActionResult> insertReturned([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("spd_InsertReturned", decrypted);

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }
        
        [HttpPost("insertTransfer")]
        public async Task<IActionResult> insertTransfer([FromBody] EncryptedRequest request)
        {
            try
            {

                 var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("spd_InsertTransfer", decrypted);
                
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }


    }
}