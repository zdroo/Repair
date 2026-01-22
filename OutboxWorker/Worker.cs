using Repair.Infrastructure.Data;

namespace OutboxWorker
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _scopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<RepairDbContext>();

                var messages = await db.OutboxMessages
                    .Where(m => m.ProcessedOn == null)
                    .OrderBy(m => m.OccurredOn)
                    .Take(20)
                    .ToListAsync(stoppingToken);

                foreach (var message in messages)
                {
                    // publish to RabbitMQ / Kafka / service bus
                    message.ProcessedOn = DateTime.UtcNow;
                }

                await db.SaveChangesAsync(stoppingToken);
                await Task.Delay(2000, stoppingToken);
            }
        }

    }
}
