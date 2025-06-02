using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.EventRequests
{
    public class GetEventByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
