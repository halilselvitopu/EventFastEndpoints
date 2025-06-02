using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Responses.OrganizerResponses
{
    public class OrganizerResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ContactInfo ContactInfo { get; set; }
    }
}
