using Microsoft.AspNetCore.SignalR;
using TechMed.API.Services;

namespace TechMed.API.NotificationHub
{

    public enum enumSignRNotificationType
    {
        BeginDialingCall,
        CallRejected,
        NotifyParticipientToJoin,
        NotifyParticipientToExit,

        CallRoomStartingForDoctor,
        CallRoomStartingForPHC,
        MeetingRoomCloseByDoctor,
        LogoutFromOtherDevices
    }
    public class SignalRNotificationModel
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public string receiverEmail { get; set; }
        public string senderEmail { get; set; }
        public long patientCaseId { get; set; }
        public string roomName { get; set; }
        public int patientId { get; set; }
    }

    public interface IHubClient
    {
        Task BroadcastMessage(SignalRNotificationModel signalRNotificationModel);
    }

    public class SignalRBroadcastHub : Hub<IHubClient>
    {
        private IUserService _userService;

        public SignalRBroadcastHub(IUserService userService)
        {
            _userService = userService;
        }
        public async Task onCallRejected(string toUser, string fromUser)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "",
                messageType = enumSignRNotificationType.CallRejected.ToString(),
                receiverEmail = toUser,
                senderEmail = fromUser
            });
        }
        public async Task onCallAcceptedByPHC(int patientCaseId, string toUser, string fromUser)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "Call Accepted by PHC",
                messageType = enumSignRNotificationType.CallRoomStartingForDoctor.ToString(),
                receiverEmail = toUser,
                senderEmail = fromUser,
                patientCaseId = patientCaseId,
            });
        }
        public async Task onMeetingRoomInitiatedByDoctor(int patientCaseId, string toUser, string fromUser, string roomName)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "Call Started",
                messageType = enumSignRNotificationType.CallRoomStartingForPHC.ToString(),
                receiverEmail = toUser,
                senderEmail = fromUser,
                patientCaseId = patientCaseId,
                roomName = roomName
            });
        }
        public async Task onMeetingRoomClose(int patientCaseId, int patientId, string toUser, string fromUser)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "Call Ended By Doctor",
                messageType = enumSignRNotificationType.MeetingRoomCloseByDoctor.ToString(),
                receiverEmail = toUser,
                senderEmail = fromUser,
                patientCaseId = patientCaseId,
                patientId = patientId
            });
        }
        public async Task onLogoutFromOtherDevices(string toUser)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "You have Login to other device.",
                messageType = enumSignRNotificationType.LogoutFromOtherDevices.ToString(),
                receiverEmail = toUser,
                senderEmail = toUser,
            });
        }
        public async Task onLogoutFromBrowser(string toUser)
        {
            var users = await _userService.LogoutUsers(toUser);
        }
    }
}
