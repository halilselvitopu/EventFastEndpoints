using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.OrganizerRequests
{
    public class DeleteOrganizerRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
