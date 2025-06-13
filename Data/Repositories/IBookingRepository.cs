using Data.Entities;
using Data.Results;

namespace Data.Repositories
{
    public interface IBookingRepository
    {
        Task<RepositoryResult<BookingEntity?>> CreateAsync(BookingEntity bookingEntity);
        Task<RepositoryResult<IEnumerable<BookingEntity>?>> GetAllBookingsByUserIdAsync(string userId);
        Task<RepositoryResult<BookingEntity?>> GetByIdAsync(string userId);
    }
}