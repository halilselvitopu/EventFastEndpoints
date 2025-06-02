using EventFastEndpoints.Models;
using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.SponsorRequests
{
    public class UpdateSponsorRequest
    {
        [FromRoute]
        public int Id { get; set; }

        [FromBody]
        public string Name { get; set; }
        public SponsorDetail SponsorDetails { get; set; }
    }
}
