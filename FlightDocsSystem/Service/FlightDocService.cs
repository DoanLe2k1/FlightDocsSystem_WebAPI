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

        public async Task<FlightDoc> GetFlightDoc(int flightDocId)
        {
            return await _dbContext.FlightDocs.FindAsync(flightDocId);
        }

        public async Task<List<FlightDoc>> GetAllFlightDocs()
        {
            return await _dbContext.FlightDocs.ToListAsync();
        }

        public async Task<int> AddFlightDoc(FlightDoc flightDoc)
        {
            _dbContext.FlightDocs.Add(flightDoc);
            await _dbContext.SaveChangesAsync();
            return flightDoc.FlightDocId;
        }

        public async Task UpdateFlightDoc(FlightDoc flightDoc)
        {
            _dbContext.Entry(flightDoc).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteFlightDoc(int flightDocId)
        {
            var flightDoc = await _dbContext.FlightDocs.FindAsync(flightDocId);
            if (flightDoc != null)
            {
                _dbContext.FlightDocs.Remove(flightDoc);
                await _dbContext.SaveChangesAsync();
            }
        }

        public async Task<List<FlightDoc>> SearchFlightDocs(string documentName, string type)
        {
            var query = _dbContext.FlightDocs.AsQueryable();

            if (!string.IsNullOrEmpty(documentName))
            {
                query = query.Where(f => f.DocumentName.Contains(documentName));
            }

            if (!string.IsNullOrEmpty(type))
            {
                query = query.Where(f => f.Type == type);
            }

            return await query.ToListAsync();
        }
        public void AddOrUpdateAvatar(int flightDocId, string FilePath)
        {
            var file = _dbContext.FlightDocs.FirstOrDefault(u => u.FlightDocId == flightDocId);
            if (file != null)
            {
                file.FilePath = FilePath;
                _dbContext.SaveChanges();
            }
        }

        public void DeleteAvatar(int flightDocId)
        {
            var file = _dbContext.FlightDocs.FirstOrDefault(u => u.FlightDocId == flightDocId);
            if (file != null)
            {
                file.FilePath = "";
                _dbContext.SaveChanges();
            }
        }
    }
}