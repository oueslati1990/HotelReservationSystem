using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Reservation.API.Entities;

namespace Reservation.API.Data
{
    public class ReservationContext : DbContext
    {
        public ReservationContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<ReservationEntity> Reservations { get; set; }
        public DbSet<RoomTypeInventory> RoomTypeInventories { get; set; }
    }
}
