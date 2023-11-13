using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Models
{
    public class FlightJourney
    {
        [Key]
        public int JourneyId { get; set; }
        public User User { get; set; }
        public Flight Flight { get; set; }
        public DateTime JourneyDate { get; set; }
    }
}
