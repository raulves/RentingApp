#pragma warning disable 1591
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Contracts.BLL.App;
using DAL.App.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace WebApp.Helpers
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceScopeFactory _serviceScopeFactory;

        public Worker(ILogger<Worker> logger, IServiceScopeFactory serviceScopeFactory)
        {
            _logger = logger;
            _serviceScopeFactory = serviceScopeFactory;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Started at: {time}", DateTimeOffset.Now);
            return base.StartAsync(cancellationToken);
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Service Stopped at: {time}", DateTimeOffset.Now);
            return base.StopAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                if (GetResponse())
                {
                    _logger.LogInformation("Response is OK: {time}", DateTimeOffset.Now);
                }
                else
                {
                    _logger.LogInformation("No response at: {time}", DateTimeOffset.Now);
                }
                
                await Task.Delay(3600000, stoppingToken);
            }
        }

        public bool GetResponse()
        {
            // Code goes here
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                // now do your work
                var bookings =
                    context.Bookings.Where(i => i.InvoiceId == null && i.CreatedAt.AddDays(1) <= DateTime.Now);

                foreach (var booking in bookings)
                {
                    Console.WriteLine(booking.Id);
                    context.Bookings.Remove(booking);
                }

                context.SaveChanges();
            }
            return true;
        }
        
    }
}