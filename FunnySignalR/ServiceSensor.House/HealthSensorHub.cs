using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using ServiceSensor.Common;

namespace ServiceSensor.House
{
    public class HealthSensorHub
        : Hub
    {
        public Task Broadcast(string sender, HealthInformation information)
        {
            return Clients.AllExcept(new[] { Context.ConnectionId })
                .SendAsync("Broadcast", sender, information);
        }
    }
}