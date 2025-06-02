using EventFastEndpoints.Models;

namespace EventFastEndpoints.DTOs.Requests.SponsorRequests
{
    public class CreateSponsorRequest
    {
        public string Name { get; set; }
        public SponsorDetail SponsorDetails { get; set; }
    }
}
