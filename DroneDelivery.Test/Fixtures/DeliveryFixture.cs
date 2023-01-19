using DroneDelivery.Domain;

namespace DroneDelivery.Test.Fixtures
{
    public static class DeliveryFixture
    {
        public static IEnumerable<Drone> SingleDrone()
        {
            return new List<Drone>() {
                new Drone() {
                    Name= "[A]",
                    Weight = 100
                }
            };
        }

        public static IList<Drone> SeveralDrones()
        {
            return new List<Drone>() {
                new Drone() {
                    Name= "[A]",
                    Weight = 100
                },
                new Drone() {
                    Name= "[B]",
                    Weight = 50
                },
                new Drone() {
                    Name= "[C]",
                    Weight = 30
                }
            };
        }


        public static IList<Location> Locations1()
        {
            return new List<Location>() {
                new Location() {
                    Name= "[A]",
                    Weight = 20
                },
                new Location() {
                    Name= "[B]",
                    Weight = 90
                },
                new Location() {
                    Name= "[C]",
                    Weight = 50
                },
                new Location() {
                    Name= "[D]",
                    Weight = 30
                },
                new Location() {
                    Name= "[E]",
                    Weight = 100
                }
            };
        }
    }
}
