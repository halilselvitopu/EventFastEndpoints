using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Requests.OrganizerRequests
{
    public class CreateOrganizerRequest
    {
        public string Name { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
