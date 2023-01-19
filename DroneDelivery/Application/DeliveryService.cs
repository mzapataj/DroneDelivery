using DroneDelivery.Common;

namespace DroneDelivery.Application
{
    public class DeliveryService : IDeliveryService
    {
        private readonly IDataFormatter dataFormatter;
        private readonly ITripService tripService;

        public DeliveryService(IDataFormatter _dataFormatter, ITripService _tripService)
        {
            dataFormatter = _dataFormatter;
            tripService = _tripService;
        }

        public string Execute(string filePath)
        {
            var (drones, locations) = dataFormatter.ReadFile(filePath);

            var trips = tripService.FindBestTrips(drones, locations);

            return dataFormatter.OutputTrips(trips);
        }

    }
}
