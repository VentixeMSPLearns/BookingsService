using Business.Models;
using Business.Results;

namespace Business.Services
{
    public interface IBookingService
    {
        Task<ServiceResult<BookingModel>> CreateBookingAsync(string userId, string eventId);
        Task<ServiceResult<IEnumerable<BookingModel>>> GetAllBookingsByUserIdAsync(string userId);
        Task<ServiceResult<BookingModel>> GetBookingAsync(string Id);
    }
}