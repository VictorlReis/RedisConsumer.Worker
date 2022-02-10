using RedisConsumer.Worker;
using StackExchange.Redis;



IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton<IConnectionMultiplexer>(x => ConnectionMultiplexer.Connect(hostContext.Configuration.GetValue<string>("RedisConnection")));
    })
    .Build();

await host.RunAsync();
