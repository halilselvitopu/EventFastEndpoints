using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using EventFastEndpoints.DTOs.Responses.OrganizerResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Endpoints.EventEndpoints
{
    public class GetAllOrganizersEndpoint : EndpointWithoutRequest<List<OrganizerResponse>>
    {
        private readonly AppDbContext _db;

        public GetAllOrganizersEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/Organizers");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Tüm organizatörleri listeler.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var organizers = await _db.Organizers
            .AsNoTracking()
            .ProjectToType<OrganizerResponse>()
            .ToListAsync(ct);

            await SendAsync(organizers);
        }
    }
}
