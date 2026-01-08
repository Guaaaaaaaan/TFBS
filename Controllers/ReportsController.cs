using Microsoft.AspNetCore.Mvc;
using TFBS.Services.Interfaces;

namespace TFBS.Controllers;

[ApiController]
[Route("api/reports")]
public class ReportsController : ControllerBase
{
    private readonly IReportService _service;

    public ReportsController(IReportService service)
    {
        _service = service;
    }

    [HttpGet("mileage/vehicle")]
    public async Task<IActionResult> MileageByVehicle([FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            return Ok(await _service.MileageByVehicleAsync(year, month));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("mileage/department")]
    public async Task<IActionResult> MileageByDepartment([FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            return Ok(await _service.MileageByDepartmentAsync(year, month));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpGet("revenue/department")]
    public async Task<IActionResult> RevenueByDepartment([FromQuery] int year, [FromQuery] int month)
    {
        try
        {
            return Ok(await _service.RevenueByDepartmentAsync(year, month));
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
