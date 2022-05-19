using TechMed.BL.TwilioAPI.Model;
using Twilio.Base;
using Twilio.Rest.Video.V1;

namespace TechMed.BL.TwilioAPI.Service
{
    public interface IVideoService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
        Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl);
        Task<ResourceSet<CompositionResource>> GetAllCompletedComposition();
        Task<CompositionResource> ComposeVideo(string roomSid, string callBackUrl);
        void DownloadComposeVideo(string compositionSid);
        Task<bool> DeleteComposeVideo(string compositionSid);
        Task<string> GetRoomSid(string roomName);
    }
}
