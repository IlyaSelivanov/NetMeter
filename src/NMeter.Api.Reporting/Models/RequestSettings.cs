namespace NMeter.Api.Reporting.Models
{
    public record FilterBy(string filterPtoperty, object filterValue);

    public class RequestSettings
    {
        public required FilterBy FilterBy { get; set; }
        
        public required string SortBy { get; set; }

        public int PageIndex { get; set; }
    }
}