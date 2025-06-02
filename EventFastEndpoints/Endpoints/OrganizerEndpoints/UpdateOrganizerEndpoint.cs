using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.OrganizerRequests;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

public class UpdateOrganizerEndpoint : Endpoint<UpdateOrganizerRequest>
{
    private readonly AppDbContext _db;

    public UpdateOrganizerEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Put("/Organizers/{id}");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Organizatör bilgilerini günceller.";

        });
    }

    public override async Task HandleAsync(UpdateOrganizerRequest req, CancellationToken ct)
    {
        var existing = await _db.Organizers
            .FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update fields
        existing.Name = req.Name;
        existing.ContactInfo = req.ContactInfo;

        await _db.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
