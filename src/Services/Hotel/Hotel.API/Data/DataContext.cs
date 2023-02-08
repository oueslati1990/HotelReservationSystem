using Hotel.API.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public virtual DbSet<HotelEntity> Hotels { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }
        public virtual DbSet<RoomType> RoomTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(h => h.HotelEntity)
                      .WithMany(r => r.Rooms)
                      .HasForeignKey(x => x.HotelId)
                      .OnDelete(DeleteBehavior.Cascade)
                      .HasConstraintName("FK_Room_Hotel");

                entity.HasOne(rt => rt.RoomType)
                      .WithMany(r => r.Rooms)
                      .HasForeignKey(x => x.RoomTypeId)
                      .OnDelete(DeleteBehavior.Restrict)
                      .HasConstraintName("FK_Room_RoomType");
            });
        }
    }
}
