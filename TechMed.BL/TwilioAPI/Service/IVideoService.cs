using TechMed.BL.TwilioAPI.Model;
using Twilio.Rest.Video.V1;

namespace TechMed.BL.TwilioAPI.Service
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
        Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl);
    }
}
