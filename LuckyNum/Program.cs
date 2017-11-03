using System;

namespace LuckyNum
{
    class Program
    {
        static void Main(string[] args)
        {
            Random randomizer=new Random();
            var num=randomizer.Next(1,100);
            Console.WriteLine("Merhaba\nBugünkü şanslı numaran\n{0}",num);
        }
    }
}