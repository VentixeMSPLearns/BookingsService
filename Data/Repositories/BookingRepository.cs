using Data.Context;
using Data.Entities;
using Data.Results;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories;

public class BookingRepository : IBookingRepository
{
    protected readonly DataContext _context;
    protected readonly DbSet<BookingEntity> _bookings;
    public BookingRepository(DataContext context)
    {
        _context = context;
        _bookings = _context.Set<BookingEntity>();
    }
    //CreateAsync
    public async Task<RepositoryResult<BookingEntity?>> CreateAsync(BookingEntity bookingEntity)
    {
        try
        {
            var result = await _bookings.AddAsync(bookingEntity);
            var saveResult = await _context.SaveChangesAsync();

            if (saveResult > 0)
            {
                return new RepositoryResult<BookingEntity?>
                {
                    Success = true,
                    Data = result.Entity,
                };
            }
            else
            {
                return new RepositoryResult<BookingEntity?>
                {
                    Success = false,
                    ErrorMessage = "No changes were saved to the database."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult<BookingEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while creating the booking entity: {ex.Message}"
            };
        }
    }
    //GetBookingById
    public async Task<RepositoryResult<BookingEntity?>> GetByIdAsync(string Id)
    {
        try
        {
            var bookingEntity = await _bookings.FirstOrDefaultAsync(b => b.Id == Id);
            if (bookingEntity != null)
            {
                return new RepositoryResult<BookingEntity?>
                {
                    Success = true,
                    Data = bookingEntity
                };
            }
            else
            {
                return new RepositoryResult<BookingEntity?>
                {
                    Success = false,
                    ErrorMessage = "Booking not found."
                };
            }
        }
        catch (Exception ex)
        {
            return new RepositoryResult<BookingEntity?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the booking entity: {ex.Message}"
            };
        }
    }
    //GetAllBookingsByUserId
    public async Task<RepositoryResult<IEnumerable<BookingEntity>?>> GetAllBookingsByUserIdAsync(string userId)
    {
        IEnumerable<BookingEntity> userBookings = [];
        try
        {
            var result = await _bookings.Where(b => b.UserId == userId).ToListAsync();
            if (result.Count > 0)
            {
                return new RepositoryResult<IEnumerable<BookingEntity>?>
                {
                    Success = true,
                    Data = result,
                };
            }

            return new RepositoryResult<IEnumerable<BookingEntity>?>
            {
                Success = false,
                ErrorMessage = "No bookings found."
            };
        }
        catch (Exception ex)
        {
            return new RepositoryResult<IEnumerable<BookingEntity>?>
            {
                Success = false,
                ErrorMessage = $"An error occurred while retrieving the user's bookings: {ex.Message}"
            };
        }
    }
}
