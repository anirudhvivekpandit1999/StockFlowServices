using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
// ...existing code...
using StockFlowService.Services;
using static StockFlowService.Controllers.StockInAndOutInboundOutboundMovementController;

// Add this for email helper
using StockFlowService.Helpers;

namespace StockFlowService.Controllers
{
    [Route("api/[controller]")]
    public class NotificationsAndAlertsController : Controller
    {
        private readonly ProcedureService _service;

        // Example: flag-based email sending
        // private static void SendFlagBasedEmail( EncryptedRequest request)
        // {
        //     var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //     if (int.Parse(decrypted["Flag"].ToString()!) == 1)
        //     {
        //         // You can customize recipient, subject, and body as needed
        //         EmailHelper.SendEmail(
        //             "anirudhvpandit.2152@gmail.com", // TODO: Replace with actual recipient
        //             "Purchase Order",   // TODO: Customize subject
        //             $"Hi Sir , we request  order for {decrypted["ProductName"]} of {decrypted["Count"]} from {decrypted["Name"]} . Please review." // TODO: Customize body
        //         );
        //     }
        //     else if (int.Parse(decrypted["Flag"].ToString()!) == 2)
        //     {
        //         EmailHelper.SendEmail(
        //             "anirudhvpandit.2152@gmail.com", // TODO: Replace with actual recipient
        //             "Sales Order",   // TODO: Customize subject
        //             $"Hi Sir , we have recieved a sales order for {decrypted["ProductName"]} of {decrypted["Count"]} from {decrypted["Name"]} . Please review." // TODO: Customize body
        //         );
        //     }
        // }

        public NotificationsAndAlertsController(ProcedureService service)
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

        // [HttpPost("GetInventoryList")]
        // public async Task<IActionResult> GetInventoryList([FromBody] EncryptedRequest request)
        // {
        //     try
        //     {

        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //         //Console.WriteLine(decrypted["Password"]);
        //         var result = await _service.CallStoredProcedureAsync("spd_FetchInventoryList", decrypted);
        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }

        // [HttpPost("GetInventoryDetails")]
        // public async Task<IActionResult> GetInventoryDetails([FromBody] EncryptedRequest request) //[FromBody] EncryptedRequest request
        // {
        //     try
        //     {

        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //         //Console.WriteLine(decrypted["Password"]);
        //         var result = await _service.CallStoredProcedureAsync("spd_FetchInventoryDetails", decrypted);
        //         //new Dictionary<string, object> { }

        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }


        //[HttpPost("TestReportData")]
        //public async Task<IActionResult> TestReportData([FromBody] EncryptedRequest request) //[FromBody] EncryptedRequest request
        //{
        //    try
        //    {

        //        var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //        //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //        //Console.WriteLine(decrypted["Password"]);
        //        var result = await _service.CallStoredProcedureAsync("spd_TestReportData", decrypted);
        //        //new Dictionary<string, object> { }

        //        var encrypted = CryptoHelper.EncryptData(result);

        //        return Ok(new { encryptedData = encrypted });
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //    }
        //}

        // [HttpPost("GetAllClientsAndSuppliersData")]
        // public async Task<IActionResult> GetAllClientsAndSuppliersData([FromBody] EncryptedRequest request) //[FromBody] EncryptedRequest request
        // {
        //     try
        //     {

        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //         //Console.WriteLine(decrypted["Password"]);
        //         var result = await _service.CallStoredProcedureAsync("spd_GetAllClientsAndSuppliersData", decrypted);
        //         //new Dictionary<string, object> { }

        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }

        // [HttpPost("GetClientAndSupplierHistory")]
        // public async Task<IActionResult> GetClientAndSupplierHistory([FromBody] EncryptedRequest request) //[FromBody] EncryptedRequest request
        // {
        //     try
        //     {

        //         var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
        //         //decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
        //         //Console.WriteLine(decrypted["Password"]);
        //         var result = await _service.CallStoredProcedureAsync("spd_GetClientAndSupplierHistory", decrypted);
        //         //new Dictionary<string, object> { }

        //         var encrypted = CryptoHelper.EncryptData(result);

        //         return Ok(new { encryptedData = encrypted });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error calling procedure: {ex.Message}");
        //     }
        // }

        // [HttpPost("SendOrderRequest")]
        // public  IActionResult SendOrderRequest([FromBody] EncryptedRequest request)
        // {
        //     try
        //     {

        //         // Process the decrypted data as needed
        //         // For example, you might want to call a service to handle the order request

        //         // Send an email notification
        //         SendFlagBasedEmail(request);


        //         return Ok( "Order request processed successfully.");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"Error processing order request: {ex.Message}");
        //     }
        // }

        [HttpPost("GetNotifications")]
        public async Task<IActionResult> GetNotifications([FromBody] EncryptedRequest request)
        {
            try
            {

                var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
                // decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                // Console.WriteLine(decrypted["Password"]);
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
                // decrypted["Password"] = CryptoHelper.EncryptData(decrypted["Password"]);
                // Console.WriteLine(decrypted["Password"]);
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