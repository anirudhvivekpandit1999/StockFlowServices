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

namespace StockFlowService.Controllers
{
    [Route("api/[controller]")]
    public class OrderManagementController : Controller
    {
        private readonly ProcedureService _service;

        private static void SendFlagBasedEmail( EncryptedRequest request)
        {
            var decrypted = CryptoHelper.DecryptData(request.EncryptedData);
            if (int.Parse(decrypted["Flag"].ToString()!) == 1)
            {
                EmailHelper.SendEmail(
                    "anirudhvpandit.2152@gmail.com", 
                    "Purchase Order",   
                    $"Hi Sir , we request  order for {decrypted["ProductName"]} of {decrypted["Count"]} from {decrypted["Name"]} . Please review."
                );
            }
            else if (int.Parse(decrypted["Flag"].ToString()!) == 2)
            {
                EmailHelper.SendEmail(
                    "anirudhvpandit.2152@gmail.com", 
                    "Sales Order",   
                    $"Hi Sir , we have recieved a sales order for {decrypted["ProductName"]} of {decrypted["Count"]} from {decrypted["Name"]} . Please review." 
                );
            }
        }

        public OrderManagementController(ProcedureService service)
        {
            _service = service;
        }

        
        [HttpPost("SendOrderRequest")]
        public  IActionResult SendOrderRequest([FromBody] EncryptedRequest request)
        {
            try
            {

                
                SendFlagBasedEmail(request);
                

                return Ok( "Order request processed successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error processing order request: {ex.Message}");
            }
        }

    }
}