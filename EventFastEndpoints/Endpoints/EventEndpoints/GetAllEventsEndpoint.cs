using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Endpoints.EventEndpoints
{
    public class GetAllEventsEndpoint : EndpointWithoutRequest<List<EventResponse>>
    {
        private readonly AppDbContext _db;

        public GetAllEventsEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/events");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Tüm etkinlikleri listeler.";
            });
        }

        public override async Task HandleAsync(CancellationToken ct)
        {
            var events = await _db.Events
            .AsNoTracking()
            .ProjectToType<EventResponse>()
            .ToListAsync(ct);

            await SendAsync(events);
        }
    }
}
