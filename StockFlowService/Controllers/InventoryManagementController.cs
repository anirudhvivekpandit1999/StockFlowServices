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

        // [HttpPost("AddNewForm")]
        // public async Task<IActionResult> AddNewForm([FromBody] EncryptedRequest request)
        // {
        //     try
        //     {
        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         Console.WriteLine("Decrypted", decrypted);
        //         var result = await _service.CallStoredProcedureAsync("spd_AddNewInboundForm", decrypted);
        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }

        // [HttpPost("GetStockFlowData")]
        // public async Task<IActionResult> GetStockFlowData([FromBody] EncryptedRequest request)
        // {
        //     try
        //     {

        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         var result = await _service.CallStoredProcedureAsync("spd_GetActivityCount", decrypted);
        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }

        //[HttpPost("Login")]
        //public async Task<IActionResult> Login([FromBody] EncryptedRequest request)
        //{
        //    try
        //    {

        //        var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //        decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //        Console.WriteLine(decrypted["Password"]);
        //        var result = await _service.CallStoredProcedureAsync("spd_Login", decrypted);
        //        var encrypted = CryptoHelper.EncryptData(result);

        //        return Ok(new { encryptedData = encrypted });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //    }
        //}

        //[HttpPost("GetAnalysis")]
        //public async Task<IActionResult> GetAnalysis() //[FromBody] EncryptedRequest request
        //{
        //    try
        //    {

        //        //var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //        //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //        //Console.WriteLine(decrypted["Password"]);
        //        var result = await _service.CallStoredProcedureAsync("spd_AnalyticsPage", new Dictionary<string, object> { });
        //        var encrypted = CryptoHelper.EncryptData(result);

        //        return Ok(new { encryptedData = encrypted });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //    }
        //}

        [HttpPost("GetInventoryList")]
        public async Task<IActionResult> GetInventoryList([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                //Console.WriteLine(decrypted["Password"]);
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
        public async Task<IActionResult> GetInventoryDetails([FromBody] EncryptedRequest request) //[FromBody] EncryptedRequest request
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                //Console.WriteLine(decrypted["Password"]);
                var result = await _service.CallStoredProcedureAsync("spd_FetchInventoryDetails", decrypted);
                //new Dictionary<string, object> { }
                
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