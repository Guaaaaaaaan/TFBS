using Microsoft.AspNetCore.Mvc;
using TFBS.Dtos.Maintenance;
using TFBS.Services.Interfaces;

namespace TFBS.Controllers;

[ApiController]
[Route("api/maintenance/logs")]
public class MaintenanceController : ControllerBase
{
    private readonly IMaintenanceService _service;

    public MaintenanceController(IMaintenanceService service) => _service = service;

    // POST /api/maintenance/logs
    [HttpPost]
    public async Task<IActionResult> CreateLog([FromBody] CreateMaintenanceLogRequest request)
    {
        var result = await _service.CreateLogAsync(request);
        return Ok(result);
    }

    // PUT /api/maintenance/logs/{logId}/complete
    [HttpPut("{logId:int}/complete")]
    public async Task<IActionResult> Complete(int logId, [FromBody] CompleteMaintenanceRequest request)
    {
        await _service.CompleteAsync(logId, request);
        return Ok(new { message = "Maintenance completed." });
    }

}
