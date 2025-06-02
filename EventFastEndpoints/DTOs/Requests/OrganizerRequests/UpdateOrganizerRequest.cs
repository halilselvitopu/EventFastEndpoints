using EventFastEndpoints.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.OrganizerRequests
{
    public class UpdateOrganizerRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public string Name { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
