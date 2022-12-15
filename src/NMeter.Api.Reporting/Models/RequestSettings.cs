namespace NMeter.Api.Reporting.Models
{
    public record FilterBy(string filterPtoperty, object filterValue);

    public record SortBy(string sortProperty, bool desc);

    public class RequestSettings
    {
        public required FilterBy FilterBy { get; set; }
        
        public required SortBy SortBy { get; set; }

        public int PageIndex { get; set; }
    }
}