using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster.Zoom;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Model.Response;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomService
    {
        Task<ZoomUserDetailDTO> GetUserDetails(string Email);
        Task<ZoomUserDetailDTO> CreateUser(string Email);
        Task<ZoomUserDetailDTO> GetUserStatusFromZoom(string Email);
        Task<NewMeetingResponseModel> CreateMeeting(string HostUserMailID, string HostAccountID);
        Task<NewMeetingResponseModel> CreateMeeting(int phcID);
        Task<bool> DeleteMeeting(string meetingID);
        Task<bool> EndMeeting(string meetingID);
        Task<bool> UpdateUserRecodingSetting(string ZoomUserID);
    }
}
