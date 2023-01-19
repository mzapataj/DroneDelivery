var builder = Host.CreateDefaultBuilder(args);
var host = builder.BuildServices();

var deliveryService = (IDeliveryService?)host.Services.GetService(typeof(IDeliveryService));
var path = "";


Console.WriteLine("\r\n   ___                      ___      ___                  \r\n  / _ \\_______  ___  ___   / _ \\___ / (_)  _____ ______ __\r\n / // / __/ _ \\/ _ \\/ -_) / // / -_) / / |/ / -_) __/ // /\r\n/____/_/  \\___/_//_/\\__/ /____/\\__/_/_/|___/\\__/_/  \\_, / \r\n                                                   /___/  \r\n");

if (args.Length == 0)
{
    Console.WriteLine("Write down the path where input file is located: ");
    path = Console.ReadLine();
}
else if (args.Length == 1 || args.Length == 2)
{    
    if (args[0] == "--path" || args[0] == "-p")
    {
        if (args[1] == null)
        {
            throw new ArgumentException($"Value missing for parameter '{args[0]}'.");
        }
        path = args[1];
    }
    else
    {
        path = args[0];        
    }
}
else
{
    throw new ArgumentException($"Too many arguments.");
}

Console.WriteLine($"Reading input file in '{path}'...");
var output = deliveryService?.Execute(path);
Console.WriteLine("Output:\n");
Console.WriteLine(output);

Console.WriteLine("Press any key to exit...");
Console.ReadKey();