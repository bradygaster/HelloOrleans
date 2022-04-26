using Abstractions;
using Orleans;

namespace Clients.WorkerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        public IClusterClient OrleansClusterClient { get; set; }

        public Worker(ILogger<Worker> logger, IClusterClient orleansClusterClient)
        {
            _logger = logger;
            OrleansClusterClient = orleansClusterClient;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await OrleansClusterClient.Connect();
            await base.StartAsync(cancellationToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await OrleansClusterClient.DisposeAsync();
            await base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var rnd = new Random();
            var randomDeviceIDs = new List<string>();

            for (int i = 0; i < 100; i++)
            {
                randomDeviceIDs.Add($"device{i.ToString().PadLeft(3,'0')}-{Environment.MachineName}");
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                var randomDeviceId = randomDeviceIDs[rnd.Next(0, 100)];
                var grain = OrleansClusterClient.GetGrain<ISensorTwinGrain>(randomDeviceId);

                await grain.ReceiveSensorState(new SensorState
                {
                    SensorId = randomDeviceId,
                    TimeStamp = DateTime.Now,
                    Type = SensorType.Unspecified,
                    Value = rnd.Next(0, 100)
                });

                await Task.Delay(100, stoppingToken);
            }
        }
    }
}