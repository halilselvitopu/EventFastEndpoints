namespace EventFastEndpoints.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public LocationInfo LocationInfo { get; set; }
        public Amenities Amenities { get; set; }

    }
}
