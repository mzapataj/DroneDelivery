namespace DroneDelivery.Test
{
    public class DataFormatterTests
    {
        private readonly IDataFormatter dataFormatter;

        public DataFormatterTests()
        {
            dataFormatter = new DataFormatter();
        }

        [Fact]
        public void InputFormat()
        {
            var path = @".\Data\Input1.txt";

            IList<Drone> drones = DeliveryFixture.SeveralDrones();

            IList<Location> locations = DeliveryFixture.Locations1();

            var dronesLocations = dataFormatter.ReadFile(path);


            Assert.Equal(drones, dronesLocations.Item1);
            Assert.Equal(locations, dronesLocations.Item2);
        }

        [Fact]
        public void OutputFormat()
        {
            var expectedOutput = "[C]\r\nTrip #1\r\n[D]\r\nTrip #2\r\n[A]\r\n\r\n[B]\r\nTrip #1\r\n[C]\r\n\r\n[A]\r\nTrip #1\r\n[E]\r\nTrip #2\r\n[B]\r\n\r\n";

            IList<Drone> drones = DeliveryFixture.SeveralDrones();

            IList<Location> locations = DeliveryFixture.Locations1();

            IList<Trip> expectedTrips = new List<Trip>()
            {
                new Trip()
                {
                    Drone = drones[2],
                    Locations = new List<Location>()
                    {
                        locations[3]
                    }
                },
                new Trip()
                {
                    Drone = drones[1],
                    Locations = new List<Location>()
                    {
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
                    Drone = drones[2],
                    Locations = new List<Location>()
                    {
                        locations[0],
                    }
                },
                new Trip()
                {
                    Drone = drones[1],
                    Locations = new List<Location>()
                },
                new Trip()
                {
                    Drone = drones[0],
                    Locations = new List<Location>()
                    {
                        locations[1]
                    }
                },
            };

            var outputStr = dataFormatter.OutputTrips(expectedTrips);


            Assert.Equal( outputStr, expectedOutput );
        }
    }
}
