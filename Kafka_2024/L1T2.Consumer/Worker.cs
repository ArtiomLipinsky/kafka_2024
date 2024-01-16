
namespace L1T2.Consumer
{
    public class Worker : BackgroundService
    {
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var tasks = Enumerable.Range(0, 3).Select(async (p) =>
                {
                    var consumer = new TaxiConsumer();
                    return consumer.Consume();
                });

                await Task.WhenAll(tasks);
            }
        }
    }
}
