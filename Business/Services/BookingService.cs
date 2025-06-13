
using Business.Models;
using Business.Results;
using Data.Entities;
using Data.Repositories;

namespace Business.Services;

public class BookingService(IBookingRepository bookingRepository) : IBookingService
{
    private readonly IBookingRepository _bookingRepository = bookingRepository;

    public async Task<ServiceResult<BookingModel>> CreateBookingAsync(string userId, string eventId)
    {
        var newBooking = new BookingEntity { UserId = userId, EventId = eventId };
        try
        {
            var result = await _bookingRepository.CreateAsync(newBooking);
            if (result.Success && result.Data != null)
            {
                return new ServiceResult<BookingModel>
                {
                    Success = true,
                    Data = new BookingModel
                    {
                        Id = result.Data.Id,
                        UserId = result.Data.UserId,
                        EventId = result.Data.EventId
                    }
                };
            }
            return new ServiceResult<BookingModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while creating the booking: {result.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<BookingModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred: {ex.Message}"
            };
        };
    }

    public async Task<ServiceResult<BookingModel>> GetBookingAsync(string Id)
    {
        try
        {
            var result = await _bookingRepository.GetByIdAsync(Id);
            if (result.Success && result.Data != null)
            {
                return new ServiceResult<BookingModel>
                {
                    Success = true,
                    Data = new BookingModel
                    {
                        Id = result.Data.Id,
                        UserId = result.Data.UserId,
                        EventId = result.Data.EventId,
                    }
                };
            }
            return new ServiceResult<BookingModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the booking: {result.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<BookingModel>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the booking: {ex.Message}"
            };
        };
    }

    public async Task<ServiceResult<IEnumerable<BookingModel>>> GetAllBookingsByUserIdAsync(string userId)
    {
        try
        {
            var result = await _bookingRepository.GetAllBookingsByUserIdAsync(userId);
            if (result.Success && result.Data!.Any())
            {
                var bookings = result.Data.Select(b => new BookingModel
                {
                    Id = b.Id,
                    UserId = b.UserId,
                    EventId = b.EventId
                }).ToList();
                return new ServiceResult<IEnumerable<BookingModel>>
                {
                    Success = true,
                    Data = bookings
                };
            }
            return new ServiceResult<IEnumerable<BookingModel>>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving bookings: {result.ErrorMessage}"
            };
        }
        catch (Exception ex)
        {
            return new ServiceResult<IEnumerable<BookingModel>>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving bookings: {ex.Message}"
            };
        };
    }
}
