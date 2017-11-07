using System;
using System.Text;
using System.Collections.Generic;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace FabrikamConsumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Consumer Tarafı\n");
            string brokerEndpoint="localhost:9092";
            Console.WriteLine("Hangi konu başlığına abone olacaksın?");
            string topicName=Console.ReadLine();

            var config=new Dictionary<string,object>{
                {"group.id","FabrikamConsumer"},
                {"bootstrap.servers",brokerEndpoint},
            };

            using(var consumer=new Consumer<Null,string>(config,null,new StringDeserializer(Encoding.UTF8)))
            {
                consumer.OnMessage+=(o,m)=>{
                    Console.WriteLine($"Duke Nukem diyor ki: {m.Value}");
                };

                consumer.Subscribe(new List<string>(){topicName});
                var isCancelled=false;
                Console.CancelKeyPress+=(_,e)=>{
                    e.Cancel=true;
                    isCancelled=true;
                };
                Console.WriteLine("Ctrl-C ile çıkabilirsin");
                while(!isCancelled)
                {
                    consumer.Poll(100);
                }
            }
        }
    }
}