using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using EventFastEndpoints.DTOs.Responses.OrganizerResponses;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Endpoints.EventEndpoints
{
    public class GetAllSponsorsEndpoint : EndpointWithoutRequest<List<SponsorResponse>>
    {
        private readonly AppDbContext _db;

        public GetAllSponsorsEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/Sponsors");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Tüm sponsorları listeler.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var Sponsors = await _db.Sponsors
            .AsNoTracking()
            .ProjectToType<SponsorResponse>()
            .ToListAsync(ct);

            await SendAsync(Sponsors);
        }
    }
}
