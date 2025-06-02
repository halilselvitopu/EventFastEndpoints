using FastEndpoints;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using Microsoft.EntityFrameworkCore;

public class GetActiveSponsorsEndpoint : EndpointWithoutRequest<List<SponsorResponse>>
{
    private readonly AppDbContext _db;

    public GetActiveSponsorsEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/sponsors/active");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Kontratı hâlâ devam eden sponsorları getirir.";

        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var activeSponsors = await _db.Sponsors
            .AsNoTracking()
            .Where(s => s.SponsorDetails.EndOfContract >= DateTime.UtcNow)
            .Select(s => new SponsorResponse
            {
                Id = s.Id,
                Name = s.Name,
                SponsorDetails = s.SponsorDetails
            })
            .ToListAsync(ct);

        await SendAsync(activeSponsors, cancellation: ct);
    }
}
