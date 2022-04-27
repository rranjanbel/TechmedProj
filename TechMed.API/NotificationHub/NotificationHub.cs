using Microsoft.AspNetCore.SignalR;

namespace TechMed.API.NotificationHub
{
    public class NotificationHub:Hub
    {
        /// <summary>
        /// Notification Hub for Room updated 
        /// </summary>
        /// <param name="flag"></param>
        /// <returns></returns>
        public async Task RoomsUpdated(bool flag)
            => await Clients.Others.SendAsync("RoomsUpdated", flag);
    }
}
