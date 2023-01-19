namespace DroneDelivery.Domain
{
    public class Trip
    {
        public Drone? Drone { get; set; }
        public IEnumerable<Location>? Locations { get; set; }
    }
}
