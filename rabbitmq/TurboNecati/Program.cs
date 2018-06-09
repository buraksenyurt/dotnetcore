using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using RabbitMQ.Client;

namespace TurboNecati
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionFactory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = connectionFactory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(queue: "commands",
                                         durable: false,
                                         exclusive: false,
                                         autoDelete: false,
                                         arguments: null);

                    var messages = new List<string>{
                                "Matematik çalış",
                                "Haftada bir kitap bitir",
                                "Felsefeyi tanı",
                                "Havalar sıcak sokak kapısına bir kap su bırak"
                            };

                    foreach (var message in messages)
                    {
                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(exchange: "",
                                         routingKey: "commands",
                                         basicProperties: null,
                                         body: body);
                        Console.WriteLine($"'{message}' gönderildi");
                        Thread.Sleep(300);
                    }
                }
            }

            Console.WriteLine(" Şimdilik bu kadar. Görüşürüz.");
            Console.ReadLine();
        }
    }
}
