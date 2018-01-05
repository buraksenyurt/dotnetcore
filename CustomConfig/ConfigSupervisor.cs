using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

public class ConfigSupervisor
{
    public IConfigurationRoot ConfigurationManager { get; set; }

    public void ExecuteJsonSample()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("aws.json");

        ConfigurationManager = builder.Build();

        Console.WriteLine($"default_region: \t{ConfigurationManager["default_region"]}");
        Console.WriteLine($"provider: \t{ConfigurationManager["provider"]}");
        Console.WriteLine($"region name: \t{ConfigurationManager["region:name"]}");
        Console.WriteLine($"region address: \t{ConfigurationManager["region:address"]}");
        Console.WriteLine($"service[1] address : \t{ConfigurationManager["services:1:address"]}");
        Console.WriteLine($"service[1] type: \t{ConfigurationManager["services:1:response_type"]}");
        Console.WriteLine($"service[1] isPublic: \t{ConfigurationManager["services:1:isPublic"]}");

        var services = ConfigurationManager.GetSection("services").AsEnumerable();
        foreach (var service in services)
        {
            Console.WriteLine($"{service.Key}-{service.Value}");
        }
    }
    public void ExecuteInMemorySample()
    {
        var builder = new ConfigurationBuilder();

        var parameters = new Dictionary<string, string>{
                {"Region:Name","east-us-2"},
                {"Region:BaseAddress","amazon.da.bir.yer/west-world/api"},
                {"Artifact:Service:Name","products"},
                {"Artifact:Service:MaxConcurrentCall","3500"},
                {"Artifact:Service:Type","json"},
                {"Artifact:Service:IsPublic","true"}
            };

        builder.AddInMemoryCollection(parameters);
        ConfigurationManager = builder.Build();
        Console.WriteLine($"{ConfigurationManager["Artifact:Service:Name"]}");
        Console.WriteLine($"{ConfigurationManager["Artifact:Service:Type"]}");
        Console.WriteLine($"{ConfigurationManager["Artifact:Service:MaxConcurrentCall"]}");
        Console.WriteLine($"{ConfigurationManager["Artifact:Service:IsPublic"]}");

        var service = new Service();
        ConfigurationManager.GetSection("Artifact:Service").Bind(service);
        Console.WriteLine($"{service.Name},{service.MaxConcurrentCall},{service.Type},{service.IsPublic}");
    }

    public void ExecuteCommandLineSample(string[] args = null)
    {
        var builder = new ConfigurationBuilder();
        var connection = new Dictionary<string, string>{
                {"Connection:Value","data source=aws;provider:amazon;"},
                {"Connection:Name","aws"}
            };
        builder
        .AddInMemoryCollection(connection)
        .AddCommandLine(args);

        ConfigurationManager = builder.Build();
        Console.WriteLine($"Connection : {ConfigurationManager["Connection:Value"]}");
        Console.WriteLine($"Connection : {ConfigurationManager["Connection:Name"]}");
    }

    public void ExecuteObjectGraphSample()
    {
        var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("gamesettings.json");

        ConfigurationManager = builder.Build();

        var gameConfig = new GameSetting();
        ConfigurationManager.GetSection("Game").Bind(gameConfig);
        var requirement = gameConfig.Requirement;
        Console.WriteLine($"OS {requirement.OS} ({requirement.RAM} Ram)");
        foreach (var contact in gameConfig.Contacts)
        {
            Console.WriteLine($"{contact.Name}({contact.Email})");
        }
    }
}