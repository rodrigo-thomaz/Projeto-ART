using Microsoft.AspNet.SignalR;

namespace ART.Domotica.DistributedServices.Hubs
{
    public class DeviceHub : Hub
    {
        public void Hello()
        {
            Clients.All.hello("Bem vindo Device!");
        }

        public void SendMessage(string message)
        {
            string completeMessage = string.Concat(Context.ConnectionId
                , " has registered the following message: ", message);

            Clients.All.registerMessage(completeMessage);
        }
    }
}