using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace FlightDocsSystem.Data
{
    public class FlightDocsSystemWebAPIDbContext : DbContext
    {
        public FlightDocsSystemWebAPIDbContext(DbContextOptions<FlightDocsSystemWebAPIDbContext> options) : base(options)
        {

        }
        public DbSet<FlightDoc> FlightDocs { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        
    }
}
