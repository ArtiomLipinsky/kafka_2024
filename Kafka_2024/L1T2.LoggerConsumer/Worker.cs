using Lib;

namespace L1T2.LoggerConsumer
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IConsumer _consumer;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;
            _consumer = new Consumer(GlobalConfig.OutputTopicsL1T2);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {

                var dist = _consumer.Consume();

                if (_logger.IsEnabled(LogLevel.Information))
                {
                    _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                }
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
