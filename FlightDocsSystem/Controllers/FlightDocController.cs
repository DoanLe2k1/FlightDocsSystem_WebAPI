using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using FlightDocsSystem.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using System;

namespace FlightDocsSystem.Controllers
{
    public class FlightDocController : Controller
    {
        private readonly IFlightDocService _flightDocService;
        public static IWebHostEnvironment _environment;

        public FlightDocController(IFlightDocService flightDocService, IWebHostEnvironment webHostEnvironment)
        {
            _flightDocService = flightDocService;
            _environment = webHostEnvironment;
        }
        [HttpGet("List FlightDocs")]
        public async Task<ActionResult<List<FlightDoc>>> GetAllFlightDocs()
        {
            var flightDocs = await _flightDocService.GetAllFlightDocs();
            return flightDocs;
        }
        [HttpGet("Search by Id")]
        public async Task<ActionResult<FlightDoc>> GetFlightDoc(int flightDocId)
        {
            var flightDoc = await _flightDocService.GetFlightDoc(flightDocId);
            if (flightDoc == null)
            {
                return NotFound();
            }
            return flightDoc;
        }
        [HttpGet("Search by Docunment, Type")]
        public async Task<ActionResult<List<FlightDoc>>> SearchFlightDocs(string documentName, string type)
        {
            var flightDocs = await _flightDocService.SearchFlightDocs(documentName, type);
            return flightDocs;
        }
        

        [HttpPost("Add FlightDoc")]
        public async Task<ActionResult<int>> AddFlightDoc(FlightDoc flightDoc)
        {
            var flightDocId = await _flightDocService.AddFlightDoc(flightDoc);
            return CreatedAtAction(nameof(GetFlightDoc), new { flightDocId }, flightDocId);
        }

        [HttpPut("Update FlightDoc")]
        public async Task<IActionResult> UpdateFlightDoc(int flightDocId, FlightDoc flightDoc)
        {
            if (flightDocId != flightDoc.FlightDocId)
            {
                return BadRequest();
            }

            await _flightDocService.UpdateFlightDoc(flightDoc);

            return NoContent();
        }

        [HttpDelete("Delete FlightDoc")]
        public async Task<IActionResult> DeleteFlightDoc(int flightDocId)
        {
            await _flightDocService.DeleteFlightDoc(flightDocId);
            return NoContent();
        }

        [HttpPost("Add-Update File")]
        public IActionResult AddOrUpdateAvatar(int flightDocId, IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            // Tạo tên file duy nhất
            string uniqueFileName = Path.GetFileNameWithoutExtension(file.FileName)
                + "_" + Guid.NewGuid().ToString().Substring(0, 8)
                + Path.GetExtension(file.FileName);

            // Xác định thư mục lưu trữ Avatar (ví dụ: wwwroot/Avatars)
            string uploadsFolder = Path.Combine(_environment.WebRootPath, "FlightDoc", "File");

            // Tạo thư mục nếu không tồn tại
            Directory.CreateDirectory(uploadsFolder);

            // Đường dẫn đầy đủ của file avatar
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            // Lưu file avatar vào thư mục đã chỉ định
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            // Gọi phương thức AddOrUpdateAvatar trong repository
            _flightDocService.AddOrUpdateAvatar(flightDocId, filePath);

            return Ok();
        }
        [HttpDelete("Delete File")]
        public IActionResult DeleteAvatar(int flightDocId)
        {
            // Gọi phương thức RemoveAvatar trong repository
            _flightDocService.DeleteAvatar(flightDocId);

            return Ok();
        }

    }
}