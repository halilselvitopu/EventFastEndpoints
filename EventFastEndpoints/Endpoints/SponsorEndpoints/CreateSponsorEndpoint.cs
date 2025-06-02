using FastEndpoints;
using EventFastEndpoints.DTOs.Requests.SponsorRequests;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using EventFastEndpoints.Models;
using EventFastEndpoints.DTOs.Responses.EventResponses;

namespace SponsorFastEndpoints.Endpoints.SponsorEndpoints;

public class CreateSponsorEndpoint : Endpoint<CreateSponsorRequest, CreateSponsorResponse>
{
    private readonly AppDbContext _db;

    public CreateSponsorEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Post("/Sponsors");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Yeni bir sponsor oluşturur.";
        });
    }

    public override async Task HandleAsync(CreateSponsorRequest req, CancellationToken ct)
    {
        var newSponsor = new Sponsor
        {
            Name = req.Name,
            SponsorDetails = req.SponsorDetails,
        };

        await _db.Sponsors.AddAsync(newSponsor, ct);
        await _db.SaveChangesAsync(ct);

        await SendAsync(new CreateSponsorResponse
        {
            Message = "Sponsor başarıyla eklendi."
        });
    }
}
