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
    public class SupplierAndClientManagementController : Controller
    {
        private readonly ProcedureService _service;

        public SupplierAndClientManagementController(ProcedureService service)
        {
            _service = service;
        }

        
        [HttpPost("GetAllClientsAndSuppliersData")]
        public async Task<IActionResult> GetAllClientsAndSuppliersData([FromBody] EncryptedRequest request) 
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_GetAllClientsAndSuppliersData", decrypted);

                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("GetClientAndSupplierHistory")]
        public async Task<IActionResult> GetClientAndSupplierHistory([FromBody] EncryptedRequest request) 
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_GetClientAndSupplierHistory", decrypted);

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