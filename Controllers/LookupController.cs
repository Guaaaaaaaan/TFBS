using Microsoft.AspNetCore.Mvc;
using TFBS.Services.Interfaces;

namespace TFBS.Controllers;

[ApiController]
[Route("api/lookups")]
public class LookupController : ControllerBase
{
    private readonly ILookupService _service;

    public LookupController(ILookupService service)
    {
        _service = service;
    }

    [HttpGet("departments")]
    public async Task<IActionResult> GetDepartments()
        => Ok(await _service.GetDepartmentsAsync());

    [HttpGet("faculties")]
    public async Task<IActionResult> GetFaculties([FromQuery] int? deptId)
        => Ok(await _service.GetFacultiesAsync(deptId));

    [HttpGet("vehicle-types")]
    public async Task<IActionResult> GetVehicleTypes()
        => Ok(await _service.GetVehicleTypesAsync());

    [HttpGet("vehicles")]
    public async Task<IActionResult> GetVehicles([FromQuery] int? typeId)
        => Ok(await _service.GetVehiclesAsync(typeId));

    [HttpGet("mechanics")]
    public async Task<IActionResult> GetMechanics([FromQuery] bool? onlyInspectors)
        => Ok(await _service.GetMechanicsAsync(onlyInspectors));
}
