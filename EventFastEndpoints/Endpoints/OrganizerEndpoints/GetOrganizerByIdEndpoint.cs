using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.OrganizerRequests;
using EventFastEndpoints.DTOs.Responses.OrganizerResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace OrganizerFastEndpoints.Endpoints.OrganizerEndpoints
{
    public class GetOrganizerByIdEndpoint : Endpoint<GetOrganizerByIdRequest, OrganizerResponse>
    {
        private readonly AppDbContext _db;

        public GetOrganizerByIdEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/Organizers/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Id'ye göre belirli bir organizatörü listeler.";

            });
        }

        public override async Task HandleAsync(GetOrganizerByIdRequest req, CancellationToken ct)
        {
            var response = await _db.Organizers
                .Where(e => e.Id == req.Id)
                .AsNoTracking()
                .ProjectToType<OrganizerResponse>()
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
