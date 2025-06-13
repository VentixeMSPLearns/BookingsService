using Business.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Presentation.Dtos;

namespace Presentation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookingsController(IBookingService bookingService) : ControllerBase
{
    protected readonly IBookingService _bookingService = bookingService;
    [HttpPost]
    public async Task<IActionResult> CreateBooking(NewBookingRequest request)
    {
        try
        {
            var result = await _bookingService.CreateBookingAsync(request.UserId, request.EventId);
            if (result.Success && result.Data != default)
            {
                var booking = result.Data;
                return CreatedAtAction(nameof(CreateBooking), new { userId = booking.UserId }, booking);
            }
            return Problem($"Error: {result.ErrorMessage}");

        }
        catch (Exception ex)
        {
            return Problem($"Error: {ex.Message}");
        }
    }
    [HttpGet("{bookingId}")]
    public async Task<IActionResult> GetBookingById(string bookingId)
    {
        try
        {
            var result = await _bookingService.GetBookingAsync(bookingId);
            if (result.Success && result.Data != default)
            {
                var booking = result.Data;
                return Ok(booking);
            }
            return Problem($"Error: {result.ErrorMessage}");
        }
        catch (Exception ex)
        {
            return Problem($"Error: {ex.Message}");
        }
    }
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetAllBookingsByUserId(string userId)
    {
        try
        {
            var result = await _bookingService.GetAllBookingsByUserIdAsync(userId);
            if (result.Success && result.Data != null)
            {
                return Ok(result.Data);
            }
            return Problem($"Error: {result.ErrorMessage}");
        }
        catch (Exception ex)
        {
            return Problem($"Error: {ex.Message}");
        }
    }
}
