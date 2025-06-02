using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.EventRequests;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using FastEndpoints;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Endpoints.EventEndpoints
{
    public class GetEventByIdEndpoint : Endpoint<GetEventByIdRequest, EventResponse>
    {
        private readonly AppDbContext _db;

        public GetEventByIdEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Get("/events/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Id'ye göre belirli bir etkinliği listeler.";
            });
        }

        public override async Task HandleAsync(GetEventByIdRequest req, CancellationToken ct)
        {
            var response = await _db.Events
                .Where(e => e.Id == req.Id)
                .AsNoTracking()
                .ProjectToType<EventResponse>()
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
