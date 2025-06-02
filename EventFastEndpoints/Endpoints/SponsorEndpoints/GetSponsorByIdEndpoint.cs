using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.SponsorRequests;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace SponsorFastEndpoints.Endpoints.SponsorEndpoints
{
    public class GetSponsorByIdEndpoint : Endpoint<GetSponsorByIdRequest, SponsorResponse>
    {
        private readonly AppDbContext _db;

        public GetSponsorByIdEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/Sponsors/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Id'ye göre belirli bir sponsoru listeler.";

            });
        }

        public override async Task HandleAsync(GetSponsorByIdRequest req, CancellationToken ct)
        {
            var response = await _db.Sponsors
                .Where(e => e.Id == req.Id)
                .AsNoTracking()
                .ProjectToType<SponsorResponse>()
                .FirstOrDefaultAsync(ct);

            if (response is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            await SendAsync(response);
        }
    }
}
