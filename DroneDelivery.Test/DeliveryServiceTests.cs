using DroneDelivery.Application;

namespace DroneDelivery.Test
{
    public class DeliveryServiceTests
    {
        private readonly IDeliveryService deliveryService;

        public DeliveryServiceTests()
        {
            deliveryService = new DeliveryService(new DataFormatter(), new TripService());
        }
        [Fact]
        public void Execute_LargeInput()
        {
            var watch = System.Diagnostics.Stopwatch.StartNew();
            var expectedMS = 180_000; // 3 minutes
            
            var path = @".\Data\LargeInput.txt";            
            var output = deliveryService.Execute(path);
            watch.Stop();
            var elapsedMs = watch.ElapsedMilliseconds;

            Assert.True(elapsedMs < expectedMS);
        }
    }
}
