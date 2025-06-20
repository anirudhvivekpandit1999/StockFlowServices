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
    public class WarehouseLocationsController : Controller
    {
        private readonly ProcedureService _service;

        public WarehouseLocationsController(ProcedureService service)
        {
            _service = service;
        }

        
        
        [HttpPost("GetWarehouseLocations")]
        public async Task<IActionResult> GetWarehouseLocations([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData!);
                var result = await _service.CallStoredProcedureAsync("spd_GetWarehouseNames", decrypted);
                
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