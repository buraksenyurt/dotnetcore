using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace CustomConfig
{
    class Program
    {
        static void Main(string[] args)
        {
            ConfigSupervisor rubio = new ConfigSupervisor();
            rubio.ExecuteCommandLineSample(args);
            rubio.ExecuteJsonSample();
            rubio.ExecuteInMemorySample();            
            rubio.ExecuteObjectGraphSample();
        }
    }    
}