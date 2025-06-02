using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Responses.EventResponses
{
    public class EventResponse
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsUpcoming { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public Amenities Amenities { get; set; }
    }
}
