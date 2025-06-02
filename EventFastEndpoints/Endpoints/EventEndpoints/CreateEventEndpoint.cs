using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Requests.EventRequests;
using EventFastEndpoints.DTOs.Responses.EventResponses;
using EventFastEndpoints.Models;
using FastEndpoints;

namespace EventFastEndpoints.Endpoints.EventEndpoints;

public class CreateEventEndpoint : Endpoint<CreateEventRequest, CreateEventResponse>
{
    private readonly AppDbContext _db;

    public CreateEventEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/events");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Yeni bir etkinlik oluşturur.";
            s.Description = "Etkinlik bilgilerini alır ve veritabanına kaydeder.";
        });
    }

    public override async Task HandleAsync(CreateEventRequest req, CancellationToken ct)
    {
        var newEvent = new Event
        {
            Title = req.Title,
            Description = req.Description,
            Date = req.Date,
            LocationInfo = req.LocationInfo,
            Amenities = req.Amenities
        };

        await _db.Events.AddAsync(newEvent, ct);
        await _db.SaveChangesAsync(ct);

        await SendAsync(new CreateEventResponse
        {
            Message = "Etkinlik başarıyla eklendi."
        });
    }
}
