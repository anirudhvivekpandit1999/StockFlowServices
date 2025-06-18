using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StockFlowService.Services;
using static StockFlowService.Controllers.StockInAndOutInboundOutboundMovementController;

using StockFlowService.Helpers;
using Microsoft.AspNetCore.Cors;

namespace StockFlowService.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class NotificationsAndAlertsController : Controller
    {
        private readonly ProcedureService _service;

        
        public NotificationsAndAlertsController(ProcedureService service)
        {
            _service = service;
        }

        
        [HttpPost("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                
                var result = await _service.CallStoredProcedureAsync("spd_GetNotification", decrypted);
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");

            }
        }
        
        [HttpPost("DeleteNotificationById")]
        public async Task<IActionResult> DeleteNotificationById([FromBody] EncryptedRequest request)
        {
            try
            {
                
                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
               
                var result = await _service.CallStoredProcedureAsync("spd_DeleteNotificationById", decrypted);
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