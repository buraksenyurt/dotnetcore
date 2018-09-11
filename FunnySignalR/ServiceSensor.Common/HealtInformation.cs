using System;
using Newtonsoft.Json;

namespace ServiceSensor.Common
{
    public class HealthInformation
    {
        [JsonProperty("serviceName")]
        public string Name { get; set; }
        [JsonProperty("healthLevel")]
        public int Level { get; set; }
        public override string ToString() => $"{Name} [{Level}]";
    }
}