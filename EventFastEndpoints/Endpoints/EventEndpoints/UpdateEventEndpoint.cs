using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.EventRequests;
using FastEndpoints;
using Microsoft.EntityFrameworkCore;

public class UpdateEventEndpoint : Endpoint<UpdateEventRequest>
{
    private readonly AppDbContext _db;

    public UpdateEventEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Put("/events/{id}");
        AllowAnonymous();
        {
            Summary(s =>
            {
                s.Summary = "Etkinlik bilgilerini günceller.";

            });
        }
    }

    public override async Task HandleAsync(UpdateEventRequest req, CancellationToken ct)
    {
        var existing = await _db.Events
            .Include(e => e.LocationInfo)
            .Include(e => e.Amenities)
            .FirstOrDefaultAsync(e => e.Id == req.Id, ct);

        if (existing is null)
        {
            await SendNotFoundAsync(ct);
            return;
        }

        // Update fields
        existing.Title = req.Title;
        existing.Date = req.Date;
        existing.LocationInfo = req.LocationInfo;
        existing.Amenities = req.Amenities;

        await _db.SaveChangesAsync(ct);

        await SendOkAsync(ct);
    }
}
