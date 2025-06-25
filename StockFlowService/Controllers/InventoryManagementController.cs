using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockFlowService.Helpers;
using StockFlowService.Services;
using static StockFlowService.Controllers.StockInAndOutInboundOutboundMovementController;

namespace StockFlowService.Controllers
{
    [Route("api/[controller]")]
    public class InventoryManagementController : Controller
    {
        private readonly ProcedureService _service;

        public InventoryManagementController(ProcedureService service)
        {
            _service = service;
        }

        
        [HttpPost("GetInventoryList")]
        public async Task<IActionResult> GetInventoryList([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_FetchInventoryList", decrypted);
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetInventoryDetails")]
        public async Task<IActionResult> GetInventoryDetails([FromBody] EncryptedRequest request) 
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_FetchInventoryDetails", decrypted);
                
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