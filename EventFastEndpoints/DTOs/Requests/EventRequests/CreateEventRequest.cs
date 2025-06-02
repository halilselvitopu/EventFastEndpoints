using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Requests.EventRequests
{
    public class CreateEventRequest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public Amenities Amenities { get; set; }
    }
}
