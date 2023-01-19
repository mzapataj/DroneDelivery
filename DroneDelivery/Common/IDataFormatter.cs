using DroneDelivery.Domain;

namespace DroneDelivery.Common
{
    public interface IDataFormatter
    {
        string OutputTrips(IEnumerable<Trip> deliveries);
        (IEnumerable<Drone>, IEnumerable<Location>) ReadFile(string filePath);        
    }
}