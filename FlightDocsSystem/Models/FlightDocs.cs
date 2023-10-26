namespace FlightDocsSystem.Models
{
    public class FlightDoc
    {
        public int FlightDocId { get; set; }
        public string DocumentName { get; set; }
        public string Type { get; set; }
        public DateTime CreateDate { get; set; }
        public double LastedVersion { get; set; }
        public byte[] PdfFile { get; set; }
        public string FilePath { get; set; }
    }
}
