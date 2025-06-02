using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.EventRequests;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

namespace EventFastEndpoints.Endpoints.EventEndpoints
{
    public class DeleteOrganizerEndpoint : Endpoint<DeleteEventRequest>
    {
        private readonly AppDbContext _db;

        public DeleteOrganizerEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Delete("/events/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Etkinliği siler.";
            });
        }

        public override async Task HandleAsync(DeleteEventRequest req, CancellationToken ct)
        {
            var eventToDelete = await _db.Events.FirstOrDefaultAsync(e => e.Id == req.Id, ct);

            if (eventToDelete is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _db.Events.Remove(eventToDelete);
            await _db.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}
