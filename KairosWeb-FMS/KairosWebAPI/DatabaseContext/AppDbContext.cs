using KairosWebAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;
// ReSharper disable All
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

namespace KairosWebAPI.DatabaseContext
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Journey> Journeys { get; set; }
        public DbSet<JourneyDetail> JourneyDetails { get; set; }
        public DbSet<FMSLogs> FMSLogs { get; set; }
        public DbSet<Chat> Chats { get; set; }
        public DbSet<JourneyTruck> JourneyTrucks { get; set; }
        public DbSet<JourneyTruckLocation> TruckLocations { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<DriverLeave> DriverLeaves { get; set; }
        public DbSet<TimeEntryDO> TimeEntryDOs { get; set; }
        public DbSet<DriverLeaveBalance> DriverLeaveBalances { get; set; }
    }
}
