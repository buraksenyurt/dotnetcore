using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class QuoteHub 
    : Hub
{
    public string Send(string incomingQuote)
    {   
        Clients.All.InvokeAsync("GetQuote",incomingQuote);
        return $"[{Context.ConnectionId}]: {incomingQuote}";
    }
}