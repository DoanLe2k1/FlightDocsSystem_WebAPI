using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Models
{
    public class FlightDoc
    {
        [Key]
        public int FlightDocId { get; set; }
        public int FlightId { get; set; }
        public string DocumentName { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public double LastedVersion { get; set; }
        public string FilePath { get; set; }
    }
}
