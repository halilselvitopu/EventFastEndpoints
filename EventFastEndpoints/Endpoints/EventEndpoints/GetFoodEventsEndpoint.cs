using FastEndpoints;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using Microsoft.EntityFrameworkCore;

public class GetFoodEventsEndpoint : EndpointWithoutRequest<List<EventResponse>>
{
    private readonly AppDbContext _db;

    public GetFoodEventsEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/events/with-food");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Yalnızca yemek hizmeti olan etkinlikleri getirir.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var foodEvents = await _db.Events
            .AsNoTracking()
            .Where(e => e.Amenities.Food)
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

        await SendAsync(foodEvents, cancellation: ct);
    }
}
