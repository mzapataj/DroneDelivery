namespace DroneDelivery.Console
{
    public static class Startup
    {
        public static IHost BuildServices(this IHostBuilder builder )
        {
            return builder
                       .ConfigureServices((_, services) =>
                            services.AddScoped<IDeliveryService, DeliveryService>()
                                    .AddSingleton<IDataFormatter, DataFormatter>()
                                    .AddSingleton<ITripService, TripService>()
                        ).Build();
        }
    }
}
