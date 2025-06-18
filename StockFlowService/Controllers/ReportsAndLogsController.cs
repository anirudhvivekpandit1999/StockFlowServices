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
    public class ReportsAndLogsController : Controller
    {
        private readonly ProcedureService _service;

        public ReportsAndLogsController(ProcedureService service)
        {
            _service = service;
        }

        

        [HttpPost("TestReportData")]
        public async Task<IActionResult> TestReportData([FromBody] EncryptedRequest request) 
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_TestReportData", decrypted);
                
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