using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;
using System.Threading;

class Program
{
    public static void Main()
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

                var reader = new EventingBasicConsumer(channel);
                reader.Received += (m, e) =>
                {
                    var body = e.Body;
                    var message = Encoding.UTF8.GetString(body);
                    Console.WriteLine($"{message}");
                    Thread.Sleep(500);
                };
                channel.BasicConsume(queue: "commands",
                                     autoAck: true,
                                     consumer: reader);

                Console.WriteLine("Teşekkürler Necati Amca :)");
                Console.ReadLine();
            }
        }
    }
}