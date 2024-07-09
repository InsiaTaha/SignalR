using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;

namespace SignalRNotificationApp.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            // Implement your logic here
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
