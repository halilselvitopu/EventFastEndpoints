using FastEndpoints;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using Microsoft.EntityFrameworkCore;

public class GetEventsWithParkingEndpoint : EndpointWithoutRequest<List<EventResponse>>
{
    private readonly AppDbContext _db;

    public GetEventsWithParkingEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/events/with-parking");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Sadece otopark hizmeti olan etkinlikleri getirir.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var eventsWithParking = await _db.Events
            .AsNoTracking()
            .Where(e => e.Amenities.Parking)
            .Select(e => new EventResponse
            {
                Id = e.Id,
                Title = e.Title,
                Date = e.Date,
                IsUpcoming = e.Date > DateTime.UtcNow,
                LocationInfo = e.LocationInfo,
                Amenities = e.Amenities
            })
            .ToListAsync(ct);

        await SendAsync(eventsWithParking, cancellation: ct);
    }
}
