using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Controllers
{
    public class FlightDocController : Controller
    {
        private readonly IFlightDocService _flightDocService;

        public FlightDocController(IFlightDocService flightDocService)
        {
            _flightDocService = flightDocService;
        }

        [HttpGet("List")]
        public async Task<ActionResult<List<FlightDoc>>> GetAllFlightDocs()
        {
            var flightDocs = await _flightDocService.GetAllFlightDocs();
            return Ok(flightDocs);
        }

        [HttpGet("{flightDocId}")]
        public async Task<ActionResult<FlightDoc>> GetFlightDocById(int flightDocId)
        {
            var flightDoc = await _flightDocService.GetFlightDocById(flightDocId);
            if (flightDoc == null)
            {
                return NotFound();
            }
            return Ok(flightDoc);
        }

        [HttpPost("Add")]
        public async Task<IActionResult> AddFlightDoc(FlightDoc flightDoc)
        {
            await _flightDocService.AddFlightDoc(flightDoc);
            return Ok();
        }

        [HttpPut("{flightDocId}")]
        public async Task<IActionResult> UpdateFlightDoc(int flightDocId, FlightDoc flightDoc)
        {
            await _flightDocService.UpdateFlightDoc(flightDocId, flightDoc);
            return Ok();
        }

        [HttpDelete("{flightDocId}")]
        public async Task<IActionResult> DeleteFlightDoc(int flightDocId)
        {
            await _flightDocService.DeleteFlightDoc(flightDocId);
            return Ok();
        }
    }
}
