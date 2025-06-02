using FastEndpoints;
using EventFastEndpoints.Context;
using EventFastEndpoints.DTOs.Responses.SponsorResponses;
using Microsoft.EntityFrameworkCore;

public class GetExpiredSponsorsEndpoint : EndpointWithoutRequest<List<SponsorResponse>>
{
    private readonly AppDbContext _db;

    public GetExpiredSponsorsEndpoint(AppDbContext db)
    {
        _db = db;
    }

    public override void Configure()
    {
        Get("/sponsors/expired");
        AllowAnonymous();
        Summary(s =>
        {
            s.Summary = "Sadece kontratı bitmiş sponsorları getirir.";
        });
    }

    public override async Task HandleAsync(CancellationToken ct)
    {
        var expiredSponsors = await _db.Sponsors
            .AsNoTracking()
            .Where(s => s.SponsorDetails.EndOfContract < DateTime.UtcNow)
            .Select(s => new SponsorResponse
            {
                Id = s.Id,
                Name = s.Name,
                SponsorDetails = s.SponsorDetails
            })
            .ToListAsync(ct);

        await SendAsync(expiredSponsors, cancellation: ct);
    }
}
