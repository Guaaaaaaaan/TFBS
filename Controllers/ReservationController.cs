using Microsoft.AspNetCore.Mvc;
using TFBS.Dtos.Reservations;
using TFBS.Services.Interfaces;

namespace TFBS.Controllers;

[ApiController]
[Route("api/reservations")]
public class ReservationController : ControllerBase
{
    private readonly IReservationService _service;

    public ReservationController(IReservationService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationRequest request)
    {
        try
        {
            var result = await _service.CreateAsync(request);
            return Ok(result);
        }
        catch (ArgumentException ex)
        {
            // 解释：把“输入不合法”转成 400
            return BadRequest(new { message = ex.Message });
        }
    }
}
