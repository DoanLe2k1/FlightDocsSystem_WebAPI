using FlightDocsSystem.Data;
using FlightDocsSystem.Models;

namespace FlightDocsSystem.Service
{
    public class FlightJourneyService : IFlightJourneyService
    {
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;

        public FlightJourneyService(FlightDocsSystemWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public FlightJourney CreateFlightJourney(int userId, int flightId)
        {
            // Check if the user with the given userId exists and has the position of "Pilot"
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == userId && u.Position == "Pilot");
            if (user == null)
            {
                throw new Exception("User with the specified UserId and Position not found.");
            }

            // Check if the flight with the given flightId exists
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == flightId);
            if (flight == null)
            {
                throw new Exception("Flight with the specified FlightId not found.");
            }

            var flightJourney = new FlightJourney
            {
                User = user,
                Flight = flight,
                JourneyDate = DateTime.Now,
            };

            _dbContext.FlightJourneys.Add(flightJourney);
            _dbContext.SaveChanges();

            return flightJourney;
        }

        public void DeleteFlightJourney(int journeyId)
        {
            var flightJourney = _dbContext.FlightJourneys.FirstOrDefault(j => j.JourneyId == journeyId);
            if (flightJourney == null)
            {
                throw new Exception("FlightJourney with the specified JourneyId not found.");
            }

            _dbContext.FlightJourneys.Remove(flightJourney);
            _dbContext.SaveChanges();
        }

        public FlightJourney UpdateFlightJourney(int journeyId, FlightJourney updatedFlightJourney)
        {
            var flightJourney = _dbContext.FlightJourneys.FirstOrDefault(j => j.JourneyId == journeyId);
            if (flightJourney == null)
            {
                throw new Exception("FlightJourney with the specified JourneyId not found.");
            }

            // Check if the user with the updatedFlightJourney.UserId exists and has the position of "Pilot"
            var user = _dbContext.Users.FirstOrDefault(u => u.UserId == updatedFlightJourney.User.UserId && u.Position == "Pilot");
            if (user == null)
            {
                throw new Exception("User with the specified UserId and Position not found.");
            }

            // Check if the flight with the updatedFlightJourney.Flight.FlightId exists
            var flight = _dbContext.Flights.FirstOrDefault(f => f.FlightId == updatedFlightJourney.Flight.FlightId);
            if (flight == null)
            {
                throw new Exception("Flight with the specified FlightId not found.");
            }

            flightJourney.User = user;
            flightJourney.Flight = flight;
            flightJourney.JourneyDate = updatedFlightJourney.JourneyDate;

            _dbContext.SaveChanges();

            return flightJourney;
        }
    }
}
