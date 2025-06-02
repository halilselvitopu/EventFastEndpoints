using FastEndpoints;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using Microsoft.EntityFrameworkCore;

public class GetGoldSponsorsEndpoint : EndpointWithoutRequest<List<SponsorResponse>>
{
    private readonly AppDbContext _db;

    public GetGoldSponsorsEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/sponsors/gold");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Sadece Altın sponsorları listeler.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var goldSponsors = await _db.Sponsors
            .AsNoTracking()
            .Where(s => s.SponsorDetails.Package == "Altın")
            .Select(s => new SponsorResponse
            {
                Id = s.Id,
                Name = s.Name,
                SponsorDetails = s.SponsorDetails
            })
            .ToListAsync(ct);

        await SendAsync(goldSponsors, cancellation: ct);
    }
}
