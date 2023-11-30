using Microsoft.AspNetCore.SignalR;

namespace PaleBazaar.MechanistTower.Beacons
{
    public class ChatBeacon : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }
    }
}
