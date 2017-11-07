using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using Confluent.Kafka;
using Confluent.Kafka.Serialization;

namespace FabrikamProducer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Producer Tarafı\n");
            string brokerEndpoint="localhost:9092";
            var quotes=File.ReadAllLines("Quotes.txt");
            Random random=new Random();
            Console.WriteLine("Bir topic adı girer misin?");
            string topicName=Console.ReadLine();

            var config=new Dictionary<string,object>{
                {"bootstrap.servers",brokerEndpoint}
            };

            using(var producer= new Producer<Null,string>(config,null,new StringSerializer(Encoding.UTF8)))
            {
                for(;;)
                {                       
                    string message=quotes[random.Next(1,quotes.Length)-1];
                    var result=producer.ProduceAsync(topicName,null,message).GetAwaiter().GetResult();
                    Console.WriteLine($"Partition : {result.Partition} Offset : {result.Offset}\n{message}");                        
                    System.Threading.Thread.Sleep(10000);
                }
            }
        }
    }
}