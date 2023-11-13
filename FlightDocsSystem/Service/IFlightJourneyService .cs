using FlightDocsSystem.Models;

namespace FlightDocsSystem.Service
{
    public interface IFlightJourneyService
    {
        FlightJourney CreateFlightJourney(int userId, int flightId);
        void DeleteFlightJourney(int journeyId);
        FlightJourney UpdateFlightJourney(int journeyId, FlightJourney updatedFlightJourney);
    }
}
