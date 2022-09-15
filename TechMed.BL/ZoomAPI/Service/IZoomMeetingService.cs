using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Model.Response;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomMeetingService
    {
        Task<NewMeetingResponseModel> NewMeeting(NewMeetingRequestModel newMeetingRequestModel, string HostAccountID);
        Task<bool> DeleteMeeting(string meetingID);   
        Task<bool> EndMeeting(string meetingID);   
        
    }
}
