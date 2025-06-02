using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Responses.SponsorResponses
{
    public class SponsorResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SponsorDetail SponsorDetails { get; set; }
    }
}
