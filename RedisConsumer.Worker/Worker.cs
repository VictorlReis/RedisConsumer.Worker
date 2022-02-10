using StackExchange.Redis;

namespace RedisConsumer.Worker;
public class Worker : BackgroundService
{
    private readonly IConnectionMultiplexer _connectionMultiplexer;
    private readonly ILogger<Worker> _logger;

    public Worker(IConnectionMultiplexer connectionMultiplexer, ILogger<Worker> logger)
    {
        _connectionMultiplexer = connectionMultiplexer;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {

            var subscriber = _connectionMultiplexer.GetSubscriber();

            await subscriber.SubscribeAsync("messages", (channel, value) =>
            {
                _logger.LogInformation(value);
                    
            });

            await Task.Delay(1000, stoppingToken);
        }
    }
}
