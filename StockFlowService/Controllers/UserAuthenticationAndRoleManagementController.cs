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
    
    public class UserAuthenticationAndRoleManagementController : Controller
    {
        private readonly ProcedureService _service;

        public UserAuthenticationAndRoleManagementController(ProcedureService service)
        {
            _service = service;
        }


        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                var result = await _service.CallStoredProcedureAsync("spd_Login", decrypted);
                var encrypted = CryptoHelper.EncryptData(result);

                return Ok(new { encryptedData = encrypted });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error calling procedure: {ex.Message}");
            }
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                var result = await _service.CallStoredProcedureAsync("spd_SignUp", decrypted);
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