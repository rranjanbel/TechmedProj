using TechMed.BL.TwilioAPI.Model;
using Twilio.Base;
using Twilio.Rest.Video.V1;
using Twilio.Rest.Video.V1.Room;
namespace TechMed.BL.TwilioAPI.Service
{
    public interface ITwilioVideoSDKService
    {
        string GetTwilioJwt(string identity);
        Task<IEnumerable<RoomDetails>> GetAllRoomsAsync();
        Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl);
        Task<ResourceSet<CompositionResource>> GetAllCompletedComposition();
        Task<CompositionResource> ComposeVideo(string roomSid, string callBackUrl);
        Task<string> DownloadComposeVideo(string compositionSid);
        Task<bool> DeleteComposeVideo(string compositionSid);
        Task<string> GetRoomSid(string roomName);
        Task<RoomResource> EndVideoCall(string roomName);
        Task<List<RoomResource>> GetAllCompletedCall();
        Task<RoomResource> GetRoomDetailFromTwilio(string roomName);
        Task<List<ParticipantResource>> GetConnectedParticipientFromTwilio(string roomName);
        Task<RoomResource> CloseRoomAsync(string roomname);
    }
}
