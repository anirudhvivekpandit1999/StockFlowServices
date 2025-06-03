using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

[ApiController]
[Route("api/sync")]
public class SyncController : ControllerBase
{
    [HttpPost("push")]
    public IActionResult Push([FromBody] SyncPushRequest request)
    {
        return Ok();
    }

    [HttpGet("pull")]
    public IActionResult Pull()
    {
        var items = new List<object>
        {
        };
        return Ok(new { items });
    }
}

public class SyncPushRequest
{
    public List<InventoryItem> Items { get; set; }
}

public class InventoryItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Count { get; set; }
    public string SerialNumber { get; set; }
    public string Location { get; set; }
    public string LastModifiedBy { get; set; }
    public string LastModifiedOn { get; set; }
}
