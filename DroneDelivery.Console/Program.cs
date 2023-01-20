var builder = Host.CreateDefaultBuilder(args);
var host = builder.BuildServices();


var deliveryService = (IDeliveryService?)host.Services.GetService(typeof(IDeliveryService));

var asciiBanner = "\r\n   ___                      ___      ___                  \r\n  / _ \\_______  ___  ___   / _ \\___ / (_)  _____ ______ __\r\n / // / __/ _ \\/ _ \\/ -_) / // / -_) / / |/ / -_) __/ // /\r\n/____/_/  \\___/_//_/\\__/ /____/\\__/_/_/|___/\\__/_/  \\_, / \r\n                                                   /___/  \r\n";
var path = "";
var outputFilePath = @"Output\Output-{0}.txt";
var currentDirectory = Directory.GetCurrentDirectory();
var fullOutputFilePath = Path.Combine(currentDirectory, string.Format(outputFilePath, DateTime.Now.ToString("yyyy-MM-ddTHH_mm_ss")));

var directoryOutput = Path.GetDirectoryName(outputFilePath);


if (args.Length == 0)
{
    Console.WriteLine(asciiBanner);
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
    else if(args[0] == "--help" || args[0] == "-h")
    {
        Console.WriteLine(asciiBanner);
        Console.WriteLine("usage: drone-delivery [-p <path> | --path <path>] [-h | --help]");
        return;
    }
    else
    {
        Console.Error.WriteLine($"Cannot by regconized '{args[0]}' parameter");
        return;
    }
}
else
{
    throw new ArgumentException($"Too many arguments.");
}

Console.WriteLine($"Processing input file in '{path}'...");
var output = deliveryService?.Execute(path);
Console.WriteLine("Output:\n");
Console.WriteLine(output);

try
{
    Directory.CreateDirectory(directoryOutput);
}
catch (Exception)
{    
}

File.WriteAllText(fullOutputFilePath, output);

Console.WriteLine($"Output was saved in the following file: '{fullOutputFilePath}'");
Console.WriteLine();
Console.WriteLine("Press any key to exit...");
Console.ReadKey();