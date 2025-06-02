using Microsoft.AspNetCore.Mvc;
using WebApi.Interfaces;
using WebApi.Models;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingConfirmationController(IBookingEmailService bookingEmailService) : ControllerBase
{
    private readonly IBookingEmailService _bookingEmailService = bookingEmailService;

    [HttpPost("send")]
    public async Task<IActionResult> SendBookingConfirmation([FromBody] SendBookingConfirmationRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(new { Error = "Invalid booking confirmation request." });

        var result = await _bookingEmailService.SendBookingConfirmationAsync(request);
        return result.Succeeded
            ? Ok()
            : StatusCode(500, new { result.Error });
    }
}
