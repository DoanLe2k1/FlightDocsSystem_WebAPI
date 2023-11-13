using FlightDocsSystem.Models;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    public class FlightJourneyController : Controller
    {
        private readonly IFlightJourneyService _flightJourneyService;

        public FlightJourneyController(IFlightJourneyService flightJourneyService)
        {
            _flightJourneyService = flightJourneyService;
        }

        [HttpPost("Add FlightJourney"), Authorize(Roles = "Admin")]
        public ActionResult<FlightJourney> CreateFlightJourney(int userId, int flightId)
        {
            try
            {
                var flightJourney = _flightJourneyService.CreateFlightJourney(userId, flightId);
                return Ok(flightJourney);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Delete FlightJourney"), Authorize(Roles = "Admin")]
        public IActionResult DeleteFlightJourney(int journeyId)
        {
            try
            {
                _flightJourneyService.DeleteFlightJourney(journeyId);
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Update FlightJourney"), Authorize(Roles = "Admin")]
        public IActionResult UpdateFlightJourney(int journeyId, FlightJourney updatedFlightJourney)
        {
            try
            {
                var flightJourney = _flightJourneyService.UpdateFlightJourney(journeyId, updatedFlightJourney);
                return Ok(flightJourney);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
