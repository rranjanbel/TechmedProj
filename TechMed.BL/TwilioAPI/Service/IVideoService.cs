using TechMed.BL.TwilioAPI.Model;

namespace TechMed.BL.TwilioAPI.Service
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
    }
}
