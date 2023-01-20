using DroneDelivery.Common;
using DroneDelivery.Domain;
using System.Collections;

namespace DroneDelivery.Application
{
    public class TripService : ITripService
    {        
        public TripService()
        {            
        }        
        
        public IList<Trip> FindBestTrips(IEnumerable<Drone> drones, IEnumerable<Location> locations)
        {
            var dronesSorted = drones.OrderByDescending(x => x.Weight);
            locations = locations.OrderBy(x => x.Weight);

            IList<Trip> listTrips = new List<Trip>();

            IEnumerable<Location> remainingLocation = new List<Location>(locations);

            var maxCapacityDrone = dronesSorted.First();

            var candidateDrone = maxCapacityDrone.Clone() as Drone;

            var sortedDronesQueue = new Queue<Drone>(dronesSorted.Count() > 1 ? dronesSorted.Skip(1) : dronesSorted);

            while (remainingLocation.Any())
            {
                IList<Location> visitedLocations = FindBestTrip(maxCapacityDrone, remainingLocation.ToList());

                var tripCapacity = visitedLocations.Sum(x => x.Weight);

                if (sortedDronesQueue.Count > 0 && sortedDronesQueue.Peek().Weight >= tripCapacity)
                {
                    candidateDrone = sortedDronesQueue.Dequeue().Clone() as Drone;
                }

                listTrips.Add(new Trip()
                {
                    Drone = candidateDrone,
                    Locations = visitedLocations
                });
                remainingLocation = remainingLocation.Except(visitedLocations);
            }

            while (sortedDronesQueue.Count > 0)
            {
                listTrips.Add(new Trip()
                {
                    Drone = sortedDronesQueue.Dequeue(),
                    Locations = new List<Location>()
                });
            }

            var dronesList = drones.ToList();
            return listTrips.OrderBy(x => dronesList.IndexOf(x.Drone)).ToList();
        }

        private IList<Location> FindBestTrip(Drone drone,IList<Location> locations)
        {
            float highestWeightCapacity = float.MaxValue;
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

                    if (highestWeightCapacity > weightCapacity)
                    {
                        highestWeightCapacity = weightCapacity;
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
