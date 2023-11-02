using FlightDocsSystem.Models;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    public class FlightController : Controller
    {
        private readonly IFlightService _flightService;

        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }

        [HttpGet("List Flights")]
        public async Task<ActionResult<List<Flight>>> GetAllFlights()
        {
            var flights = await _flightService.GetAllFlights();
            return Ok(flights);
        }

        [HttpGet("Find Flight by Id")]
        public async Task<ActionResult<Flight>> GetFlightById(int flightId)
        {
            var flight = await _flightService.GetFlightById(flightId);
            if (flight == null)
            {
                return NotFound();
            }
            return Ok(flight);
        }

        [HttpPost("Add Flight")]
        public async Task<ActionResult> AddFlight(Flight flight)
        {
            flight.FlightDate = DateTime.Now.Date;
            flight.FlightTime = DateTime.Now.TimeOfDay;
            await _flightService.AddFlight(flight);
            return Ok();
        }

        [HttpPut("Update Flight")]
        public async Task<ActionResult> UpdateFlight(int flightId, Flight flight)
        {
            flight.FlightDate = DateTime.Now.Date;
            flight.FlightTime = DateTime.Now.TimeOfDay;
            await _flightService.UpdateFlight(flightId, flight);
            return Ok();
        }

        [HttpDelete("Delete Flight")]
        public async Task<ActionResult> DeleteFlight(int flightId)
        {
            await _flightService.DeleteFlight(flightId);
            return Ok();
        }
    }
}
