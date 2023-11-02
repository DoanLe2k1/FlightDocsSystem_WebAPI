using FlightDocsSystem.Data;
using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FlightDocsSystem.Service
{
    public class FlightDocService : IFlightDocService
    {
        private readonly FlightDocsSystemWebAPIDbContext _dbContext;
        public FlightDocService(FlightDocsSystemWebAPIDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<FlightDoc>> GetAllFlightDocs()
        {
            return await _dbContext.FlightDocs.ToListAsync();
        }
        public async Task<FlightDoc> GetFlightDocById(int flightDocId)
        {
            return await _dbContext.FlightDocs.FirstOrDefaultAsync(f => f.FlightDocId == flightDocId);
        }

        public async Task AddFlightDoc(FlightDoc flightDoc)
        {
            await _dbContext.FlightDocs.AddAsync(flightDoc);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateFlightDoc(int flightDocId, FlightDoc flightDoc)
        {
            var existingFlightDoc = await _dbContext.FlightDocs.FirstOrDefaultAsync(f => f.FlightDocId == flightDocId);
            if (existingFlightDoc != null)
            {
                existingFlightDoc.DocumentName = flightDoc.DocumentName;
                existingFlightDoc.Type = flightDoc.Type;
                existingFlightDoc.CreateDate = flightDoc.CreateDate;
                existingFlightDoc.LastedVersion = flightDoc.LastedVersion;
                existingFlightDoc.PdfFile = flightDoc.PdfFile;
                existingFlightDoc.FilePath = flightDoc.FilePath;
                await _dbContext.SaveChangesAsync();
            }
        }
        public async Task DeleteFlightDoc(int flightDocId)
        {
            var flightDoc = await _dbContext.FlightDocs.FirstOrDefaultAsync(f => f.FlightDocId == flightDocId);
            if (flightDoc != null)
            {
                _dbContext.FlightDocs.Remove(flightDoc);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}