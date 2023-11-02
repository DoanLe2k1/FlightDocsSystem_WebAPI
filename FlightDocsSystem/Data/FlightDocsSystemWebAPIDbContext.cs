﻿using CMS_WebAPI.Models;
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
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FlightDoc>()
                .HasKey(p => p.FlightDocId);
            modelBuilder.Entity<FlightDoc>()
                .Property(p => p.FlightDocId)
                .ValueGeneratedNever();
        }
    }
}
