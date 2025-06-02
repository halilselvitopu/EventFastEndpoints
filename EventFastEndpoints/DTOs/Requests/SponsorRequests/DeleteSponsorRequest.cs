using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.SponsorRequests
{
    public class DeleteSponsorRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
