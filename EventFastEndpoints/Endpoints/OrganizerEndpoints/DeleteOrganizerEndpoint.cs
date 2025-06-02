using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using EventFastEndpoints.DTOs.Requests.OrganizerRequests;
using EventFastEndpoints.Context;

namespace OrganizerFastEndpoints.Endpoints.OrganizerEndpoints
{
    public class DeleteOrganizerEndpoint : Endpoint<DeleteOrganizerRequest>
    {
        private readonly AppDbContext _db;

        public DeleteOrganizerEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Delete("/Organizers/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Organizatörü siler.";
            });
        }

        public override async Task HandleAsync(DeleteOrganizerRequest req, CancellationToken ct)
        {
            var OrganizerToDelete = await _db.Organizers.FirstOrDefaultAsync(e => e.Id == req.Id, ct);

            if (OrganizerToDelete is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _db.Organizers.Remove(OrganizerToDelete);
            await _db.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}
