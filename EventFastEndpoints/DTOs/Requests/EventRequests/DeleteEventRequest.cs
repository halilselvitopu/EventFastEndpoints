using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.EventRequests
{
    public class DeleteEventRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
