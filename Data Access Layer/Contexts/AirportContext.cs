using Data_Access_Layer.DbInitializer;
using Data_Access_Layer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Data_Access_Layer.Contexts
{
    public class AirportContext : DbContext
    {
        public AirportContext(DbContextOptions<AirportContext> options)
            : base(options)
        {

        }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Crew> Crews { get; set; }
        public virtual DbSet<Departure> Departures { get; set; }
        public virtual DbSet<Pilot> Pilots { get; set; }
        public virtual DbSet<Plane> Planes { get; set; }
        public virtual DbSet<PlaneType> PlaneTypes { get; set; }
        public virtual DbSet<Stewardess> Stewardesses { get; set; }
        public virtual DbSet<Ticket> Tickets { get; set; }

    }
}
