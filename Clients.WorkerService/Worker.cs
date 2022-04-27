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
            var randomDevices = new Dictionary<string, ISensorTwinGrain>();

            for (int i = 0; i < 256; i++)
            {
                var key = $"device{i.ToString().PadLeft(3, '0')}-{rnd.Next(10000, 99999)}-{Environment.MachineName}";
                randomDeviceIDs.Add(key);
                randomDevices.Add(key, OrleansClusterClient.GetGrain<ISensorTwinGrain>(key));
            }

            while (!stoppingToken.IsCancellationRequested)
            {
                await Parallel.ForEachAsync(randomDeviceIDs, async (deviceId, stoppingToken) =>
                {
                    await randomDevices[deviceId].ReceiveSensorState(new SensorState
                    {
                        SensorId = deviceId,
                        TimeStamp = DateTime.Now,
                        Type = SensorType.Unspecified,
                        Value = rnd.Next(0, 100)
                    });
                });
            }
        }
    }
}