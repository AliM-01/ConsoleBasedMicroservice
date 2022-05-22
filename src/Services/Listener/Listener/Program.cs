using Microsoft.Extensions.Configuration;

var builder = new ConfigurationBuilder()
                .AddJsonFile($"appsettings.json", true, true)
                .AddEnvironmentVariables();

var config = builder.Build();

Console.WriteLine("-------------------\n Listener service is up ! \n-------------------\n Press any key to exit...\n\n");

await Task.Run(() => Console.ReadKey());