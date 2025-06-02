using EventFastEndpoints.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.EventRequests
{
    public class UpdateEventRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public bool IsUpcoming { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public Amenities Amenities { get; set; }
    }

}
