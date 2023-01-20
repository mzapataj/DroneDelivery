namespace DroneDelivery.Test
{
    public class TripServiceTests
    {
        private readonly ITripService tripService;

        public TripServiceTests()
        {
            tripService =  new TripService();
        }

        [Fact]
        public void FindBestTrip_SingleDrone()
        {
            var drones = DeliveryFixture.SingleDrone();

            IEnumerable<Location> locations = DeliveryFixture.Locations1();

            IList<Trip> expectedTrips = new List<Trip>()
            {
                new Trip()
                {
                    Drone = new Drone()
                    {
                        Name= "[A]",
                        Weight = 100
                    },
                    Locations = 
                    new List<Location>() {
                        new Location() {
                            Name= "[A]",
                            Weight = 20
                        },
                        new Location() {
                            Name= "[C]",
                            Weight = 50
                        },
                        new Location() {
                            Name= "[D]",
                            Weight = 30
                        }
                    }
                }
                ,
                new Trip()
                {
                    Drone = new Drone()
                    {
                        Name= "[A]",
                        Weight = 100
                    },
                    Locations =  
                    new List<Location>() {
                        new Location() {
                            Name= "[B]",
                            Weight = 90
                        }
                    }
                }
                ,
                new Trip()
                {
                    Drone = new Drone()
                    {
                        Name= "[A]",
                        Weight = 100
                    },
                    Locations =                         
                    new List<Location>() {
                        new Location() {
                            Name= "[E]",
                            Weight = 100
                        }
                    }
                }
            };

            var trips = tripService.FindBestTrips(drones, locations);

            trips.Should().BeEquivalentTo(expectedTrips);
        }


        [Fact]
        public void FindBestTrip_SeveralDrone()
        {
            IList<Drone> drones = DeliveryFixture.SeveralDrones();

            IList<Location> locations = DeliveryFixture.Locations1();

            IList<Trip> expectedTrips = new List<Trip>()
            {
                new Trip()
                {
                    Drone = drones[0],
                    Locations = new List<Location>()
                    {
                        locations[0],
                        locations[3],
                        locations[2]
                    }
                },
                new Trip()
                {
                    Drone = drones[0],
                    Locations = new List<Location>()
                    {
                        locations[4]
                    }
                },
                new Trip()
                {
                    Drone = drones[0],
                    Locations = new List<Location>()
                    {
                        locations[1]
                    }
                },
                new Trip()
                {
                    Drone = drones[1],
                    Locations = new List<Location>()                    
                },
                new Trip()
                {
                    Drone = drones[2],
                    Locations = new List<Location>()                         
                }
            };

            var trips = tripService.FindBestTrips(drones, locations);

            trips.Should().BeEquivalentTo(expectedTrips);
        }
    }
}