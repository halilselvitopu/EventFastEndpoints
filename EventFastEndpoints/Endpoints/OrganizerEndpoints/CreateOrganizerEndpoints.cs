using FastEndpoints;
using EventFastEndpoints.DTOs.Requests.OrganizerRequests;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.OrganizerResponses;
using EventFastEndpoints.Models;
using EventFastEndpoints.DTOs.Responses.EventResponses;

namespace OrganizerFastEndpoints.Endpoints.OrganizerEndpoints;

public class CreateOrganizerEndpoint : Endpoint<CreateOrganizerRequest, CreateOrganizerResponse>
{
    private readonly AppDbContext _db;

    public CreateOrganizerEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/Organizers");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Yeni bir organizatör oluşturur";
        });
    }

    public override async Task HandleAsync(CreateOrganizerRequest req, CancellationToken ct)
    {
        var newOrganizer = new Organizer
        {
            Name = req.Name,
            ContactInfo = req.ContactInfo,
        };

        await _db.Organizers.AddAsync(newOrganizer, ct);
        await _db.SaveChangesAsync(ct);

        await SendAsync(new CreateOrganizerResponse
        {
            Message = "Organizatör başarıyla eklendi."
        });
    }
}
