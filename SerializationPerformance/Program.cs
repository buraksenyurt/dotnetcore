using System;
using BenchmarkDotNet.Running;

namespace SerializationPerformance
{
    class Program
    {
        static void Main(string[] args)
        {
            var summary = BenchmarkRunner.Run<Serializers>();
        }
    }
}
