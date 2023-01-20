using DroneDelivery.Domain;

namespace DroneDelivery.Application
{
    public interface ITripService
    {
        IList<Trip> FindBestTrips(IEnumerable<Drone> drones, IEnumerable<Location> locations);        
    }
}