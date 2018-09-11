using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;
using ServiceSensor.Common;

namespace ServiceSensor.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            var cToken = new CancellationTokenSource();
            Task.Run(() => SendInformationAsync(cToken.Token)
                            .GetAwaiter()
                            .GetResult(), cToken.Token);

            Console.WriteLine("Sonlandırmak için bir tuşa basın...");
            Console.ReadLine();
            cToken.Cancel();
        }

        static async Task SendInformationAsync(CancellationToken cToken)
        {
            var connBuilder = new HubConnectionBuilder()
                .WithUrl("http://localhost:7001/healthSensor")
                .Build();

            await connBuilder.StartAsync();
            Random rnd = new Random();
            int randomHealthPoint = 0;

            while (!cToken.IsCancellationRequested)
            {
                await Task.Delay(1000, cToken);
                randomHealthPoint = rnd.Next(1, 25);
                var information = new HealthInformation() 
                    {
                         Name = $"service_{randomHealthPoint}", 
                         Level = randomHealthPoint
                     };
                Console.WriteLine(information.ToString());
                await connBuilder.InvokeAsync("Broadcast", "HealthSensor", information, cToken);
            }

            await connBuilder.DisposeAsync();
        }
    }
}
