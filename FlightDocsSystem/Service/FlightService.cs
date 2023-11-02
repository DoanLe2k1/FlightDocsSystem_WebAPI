using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.Service
{
    public class FlightService : IFlightService
    {
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;

        public FlightService(FlightDocsSystemWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Flight>> GetAllFlights()
        {
            return await _dbContext.Flights.ToListAsync();
        }

        public async Task<Flight> GetFlightById(int flightId)
        {
            return await _dbContext.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
        }

        public async Task AddFlight(Flight flight)
        {
            _dbContext.Flights.Add(flight);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFlight(int flightId, Flight flight)
        {
            var existingFlight = await _dbContext.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
            if (existingFlight != null)
            {
                existingFlight.FlightNumber = flight.FlightNumber;
                existingFlight.DepartureAirport = flight.DepartureAirport;
                existingFlight.ArrivalAirport = flight.ArrivalAirport;
                existingFlight.FlightDate = flight.FlightDate;
                existingFlight.FlightTime = flight.FlightTime;
                existingFlight.Status = flight.Status;
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteFlight(int flightId)
        {
            var flight = await _dbContext.Flights.FirstOrDefaultAsync(f => f.FlightId == flightId);
            if (flight != null)
            {
                _dbContext.Flights.Remove(flight);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
