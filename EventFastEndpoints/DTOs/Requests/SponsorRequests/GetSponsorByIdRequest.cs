using Microsoft.AspNetCore.Mvc;

namespace EventFastEndpoints.DTOs.Requests.SponsorRequests
{
    public class GetSponsorByIdRequest
    {
        [FromRoute]
        public int Id { get; set; }
    }
}
