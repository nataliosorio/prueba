using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using System.Threading;
using System.Threading.Tasks;
using System.Numerics;

namespace Utilities.BackgroundServicess
{
    public class QueuedHostedService : BackgroundService
    {
        private readonly IBackgroundTaskQueue _taskQueue;
        private readonly IServiceProvider _serviceProvider;


        public QueuedHostedService(IBackgroundTaskQueue taskQueue, IServiceProvider serviceProvider)
        {
            _taskQueue = taskQueue;
            _serviceProvider = serviceProvider;
        }

        //protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        //{
        //    while (!stoppingToken.IsCancellationRequested)
        //    {
        //        var workItem = await _taskQueue.DequeueAsync(stoppingToken);

        //        try
        //        {
        //            await workItem(stoppingToken);
        //        }
        //        catch (Exception ex)
        //        {
        //            // manejar errores si quieres loggear
        //        }
        //    }
        //}

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("🚀 Servicio en segundo plano iniciado.");
            //_logger.LogInformation("🚀 Servicio en segundo plano iniciado.");

            while (!stoppingToken.IsCancellationRequested)
            {
                var workItem = await _taskQueue.DequeueAsync(stoppingToken);

                try
                {
                    Console.WriteLine("⚙️ Ejecutando tarea en segundo plano....");

                    //_logger.LogInformation("⚙️ Ejecutando tarea en segundo plano...");
                    await workItem(stoppingToken);
                }
                catch (Exception)
                {
                    Console.WriteLine("❌ Error ejecutando tarea en segundo plano.");
                }
            }
            Console.WriteLine("\U0001f6d1 Servicio en segundo plano detenido.");

        }

    }
}
