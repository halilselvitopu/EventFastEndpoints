using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.SponsorRequests;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

public class UpdateSponsorEndpoint : Endpoint<UpdateSponsorRequest>
{
    private readonly AppDbContext _db;

    public UpdateSponsorEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Put("/Sponsors/{id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Sponsor bilgilerini günceller.";

        });
    }

    public override async Task HandleAsync(UpdateSponsorRequest req, CancellationToken ct)
    {
        var existing = await _db.Sponsors
            .FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update fields
        existing.Name = req.Name;
        existing.SponsorDetails = req.SponsorDetails;

        await _db.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
