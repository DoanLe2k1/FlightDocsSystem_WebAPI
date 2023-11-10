using System.ComponentModel.DataAnnotations;

namespace FlightDocsSystem.Models
{
    public class Group
    {
        [Key]
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public DateTime CreateDate { get; set; } = DateTime.Now;
        public string Description { get; set; }
    }
}
