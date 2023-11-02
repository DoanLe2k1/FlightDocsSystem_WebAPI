
using FlightDocsSystem.Models;
using Microsoft.AspNetCore.Mvc;

namespace FlightDocsSystem.Service
{
    public interface IFlightDocService
    {
        Task<List<FlightDoc>> GetAllFlightDocs();
        Task<FlightDoc> GetFlightDocById(int flightDocId);
        Task AddFlightDoc(FlightDoc flightDoc);
        Task UpdateFlightDoc(int flightDocId, FlightDoc flightDoc);
        Task DeleteFlightDoc(int flightDocId);
    }
}
