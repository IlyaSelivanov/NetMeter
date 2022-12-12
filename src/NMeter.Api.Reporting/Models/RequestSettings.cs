namespace NMeter.Api.Reporting.Models
{
    public class RequestSettings
    {
        public required string FilterBy { get; set; }
        
        public required string SortBy { get; set; }

        public int PageIndex { get; set; }
    }
}