using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR.Client;

namespace FabrikamPostman
{
    class Program
    {
        static void Main(string[] args)
        {   
            HubConnection conn = new HubConnectionBuilder()
                 .WithUrl("http://localhost:5000/QuoteHub")
                 .WithConsoleLogger()
                 .Build();

            conn.StartAsync().ContinueWith(t=>{
                if(t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Connected to Hub");

            }).Wait();

            conn.On<string>("GetQuote", param => {                
            });
            
            for(int i=0;i<10;i++)
            {   
                Random random=new Random();
                int index=random.Next(0,QuoteFabric.GetQuotes().Count);
                conn.InvokeAsync<string>("Send",QuoteFabric.GetQuotes()[index].ToString())
                .ContinueWith(t=>{
                    if(t.IsFaulted)
                        Console.WriteLine(t.Exception.GetBaseException());
                    else
                        Console.WriteLine(t.Result);
                });
                Thread.Sleep(10000);
            }

            conn.DisposeAsync().ContinueWith(t=>{
                if(t.IsFaulted)
                    Console.WriteLine(t.Exception.GetBaseException());
                else
                    Console.WriteLine("Disconnected");
            });
        }
    }
}