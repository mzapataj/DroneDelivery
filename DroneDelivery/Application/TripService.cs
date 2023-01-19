using DroneDelivery.Common;
using DroneDelivery.Domain;

namespace DroneDelivery.Application
{
    public class TripService : ITripService
    {        
        public TripService()
        {            
        }        
        

        public IList<Trip> FindBestTrips(IEnumerable<Drone> drones, IEnumerable<Location> locations)
        {            
            var dronesSorted = drones.OrderBy(x => x.Weight);

            locations = locations.OrderBy(x => x.Weight);

            IList <Trip> listTrips = new List<Trip>();            

            IEnumerable<Location> remainingLocation = new List<Location>(locations);

            while (remainingLocation.Any())
            {               
                foreach (var drone in dronesSorted)
                {
                    IList<Location> visitedLocations = FindBestTrip(drone, remainingLocation.ToList());

                    listTrips.Add(new Trip()
                    {
                        Drone = drone,
                        Locations = visitedLocations
                    });

                    remainingLocation = remainingLocation.Except(visitedLocations);
                }
            }

            var dronesList = drones.ToList();
            return listTrips.OrderBy( 
                x =>
                {
                    var index = dronesList.IndexOf(x.Drone);
                    return index;
                }
                
                ).ToList();
        }

        private IList<Location> FindBestTrip(Drone drone,IList<Location> locations)
        {
            float lowestWeightCapacity = float.MaxValue;
            float weightCapacity = drone.Weight;
            bool finishSearch = false;
            bool skipCurrentLoop = false;


            IList<Location> bestCandidateTrip = new List<Location>();

            bool isBestWeigthCapacityOverpassed(ref IList<Location> visitedLocations, int i)
            {
                if (weightCapacity - locations[i].Weight >= 0)
                {
                    visitedLocations.Add(new Location()
                    {
                        Name = locations[i].Name,
                        Weight = locations[i].Weight,
                    });

                    weightCapacity -= locations[i].Weight;

                    if (lowestWeightCapacity > weightCapacity)
                    {
                        lowestWeightCapacity = weightCapacity;
                        bestCandidateTrip = new List<Location>(visitedLocations);
                    }

                    if (weightCapacity == 0)
                    {
                        finishSearch = true;                        
                    }
                    return false;
                }
                else
                {
                    return true;
                }
            }

            for (int i = 0; i < locations.Count(); i++)
            {
                IList<Location> visitedLocations = new List<Location>();
                skipCurrentLoop = isBestWeigthCapacityOverpassed(ref visitedLocations, i);                
                if (finishSearch)
                    break;
                if (skipCurrentLoop)
                    continue;

                for (int j = i + 1 ; j < locations.Count(); j++)
                {                    
                    for (int k = j; k < locations.Count(); k++)
                    {
                        skipCurrentLoop = isBestWeigthCapacityOverpassed(ref visitedLocations, k);
                        if (finishSearch)
                            return bestCandidateTrip;
                        if (skipCurrentLoop)
                        {
                            visitedLocations = new List<Location>();
                            weightCapacity = drone.Weight;
                            break;
                        }
                            
                    }
                }
            }
            return bestCandidateTrip;
        }
    }
}
