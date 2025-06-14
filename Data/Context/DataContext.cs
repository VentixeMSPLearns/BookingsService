﻿using Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Data.Context;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<BookingEntity> Bookings { get; set; } = null!;

}
