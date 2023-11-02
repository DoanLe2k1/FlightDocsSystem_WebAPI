using FlightDocsSystem.Models;

namespace FlightDocsSystem.Service
{
    public interface IFlightService
    {
        Task<List<Flight>> GetAllFlights();
        Task<Flight> GetFlightById(int flightId);
        Task AddFlight(Flight flight);
        Task UpdateFlight(int flightId, Flight flight);
        Task DeleteFlight(int flightId);
    }
}
