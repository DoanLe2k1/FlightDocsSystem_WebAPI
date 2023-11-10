using FlightDocsSystem.Models;

namespace FlightDocsSystem.Service
{
    public interface IFlightDocService
    {
        Task<FlightDoc> GetFlightDoc(int flightDocId);
        Task<List<FlightDoc>> GetAllFlightDocs();
        Task<int> AddFlightDoc(FlightDoc flightDoc);
        Task UpdateFlightDoc(FlightDoc flightDoc);
        Task DeleteFlightDoc(int flightDocId);
        Task<List<FlightDoc>> SearchFlightDocs(string documentName, string type);
        void AddOrUpdateAvatar(int flightDocId, string FilePath);
        void DeleteAvatar(int flightDocId);
    }
}
