using Microsoft.AspNetCore.SignalR;

namespace TechMed.API.NotificationHub
{

    public enum enumSignRNotificationType
    {
        CallingToPHC,
        CallRejectedByPHC,
        CallRoomStartingForDoctor,
        CallRoomStartingForPHC,
        MeetingRoomCloseByDoctor
    }
    public class SignalRNotificationModel
    {
        public string messageType { get; set; }
        public string message { get; set; }
        public string receiverEmail { get; set; }
        public string senderEmail { get; set; }
        public int patientCaseId { get; set; }
        public string roomName { get; set; }
        public int patientId { get; set; }
    }

    public interface IHubClient
    {
        Task BroadcastMessage(SignalRNotificationModel signalRNotificationModel);
    }



    public class SignalRBroadcastHub : Hub<IHubClient>
    {
        public SignalRBroadcastHub()
        {
        }
        public async Task onCallRejectedByPHC(string toUser, string fromUser)
        {
            await Clients.All.BroadcastMessage(new SignalRNotificationModel()
            {
                message = "Call Rejected by PHC",
                messageType = enumSignRNotificationType.CallRejectedByPHC.ToString(),
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
    }
}
