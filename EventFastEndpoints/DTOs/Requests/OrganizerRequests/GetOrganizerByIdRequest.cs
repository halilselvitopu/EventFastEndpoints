using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.OrganizerRequests
{
    public class GetOrganizerByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
