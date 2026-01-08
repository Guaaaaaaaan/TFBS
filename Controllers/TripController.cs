using Microsoft.AspNetCore.Mvc;
using TFBS.Dtos.Trips;
using TFBS.Services.Interfaces;

namespace TFBS.Controllers;

[ApiController]
[Route("api/trips")]
public class TripController : ControllerBase
{
    private readonly ITripService _service;

    public TripController(ITripService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateTripRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

    [HttpPut("{tripId:int}/complete")]
    public async Task<IActionResult> Complete(int tripId, [FromBody] CompleteTripRequest request)
    {
        try
        {
            var result = await _service.CompleteAsync(tripId, request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }

}
