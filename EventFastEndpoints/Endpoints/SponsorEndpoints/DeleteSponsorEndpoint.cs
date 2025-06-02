using FastEndpoints;
using Microsoft.EntityFrameworkCore;
using EventFastEndpoints.DTOs.Requests.SponsorRequests;
using EventFastEndpoints.Context;

namespace SponsorFastEndpoints.Endpoints.SponsorEndpoints
{
    public class DeleteSponsorEndpoint : Endpoint<DeleteSponsorRequest>
    {
        private readonly AppDbContext _db;

        public DeleteSponsorEndpoint(AppDbContext db)
        {
            _db = db;
        }

        public override void Configure()
        {
            Delete("/Sponsors/{id}");
            AllowAnonymous();
            Summary(s =>
            {
                s.Summary = "Sponsoru siler.";
            });
        }

        public override async Task HandleAsync(DeleteSponsorRequest req, CancellationToken ct)
        {
            var SponsorToDelete = await _db.Sponsors.FirstOrDefaultAsync(e => e.Id == req.Id, ct);

            if (SponsorToDelete is null)
            {
                await SendNotFoundAsync(ct);
                return;
            }

            _db.Sponsors.Remove(SponsorToDelete);
            await _db.SaveChangesAsync(ct);

            await SendNoContentAsync(ct);
        }
    }
}
