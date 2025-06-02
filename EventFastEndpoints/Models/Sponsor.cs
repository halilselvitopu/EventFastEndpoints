namespace EventFastEndpoints.Models
{
    public class Sponsor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public SponsorDetail SponsorDetails { get; set; }
    }
}
