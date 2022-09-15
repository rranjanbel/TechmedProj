using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster.Zoom;
using TechMed.BL.ZoomAPI.Model;
using TechMed.BL.ZoomAPI.Model.Response;
using TechMed.DL.Models;

namespace TechMed.BL.ZoomAPI.Service
{
    public class ZoomService : IZoomService
    {
        private readonly IZoomUserService _ZoomUserService;
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IZoomMeetingService _zoomMeetingService;
        public ZoomService(IZoomMeetingService zoomMeetingService, IZoomUserService ZoomUserService, TeleMedecineContext teleMedecineContext)
        {
            _ZoomUserService = ZoomUserService;
            _teleMedecineContext = teleMedecineContext;
            _zoomMeetingService = zoomMeetingService;
        }

        public async Task<NewMeetingResponseModel> CreateMeeting(string HostUserMailID, string HostAccountID)
        {
            NewMeetingResponseModel responseModel = new NewMeetingResponseModel();
            NewMeetingRequestModel newMeetingRequestModel = new NewMeetingRequestModel();
            newMeetingRequestModel.agenda = "Telemed Meeting";
            newMeetingRequestModel.duration = 30;
            newMeetingRequestModel.password = "123456";
            newMeetingRequestModel.schedule_for = HostUserMailID;
            newMeetingRequestModel.start_time = DateTime.Now;

            responseModel = await _zoomMeetingService.NewMeeting(newMeetingRequestModel, HostAccountID);
            return responseModel;
        }

        public async Task<NewMeetingResponseModel> CreateMeeting(int phcID)
        {

            Phcmaster phcmaster = await _teleMedecineContext.Phcmasters.Where(a => a.Id == phcID).FirstOrDefaultAsync();
            ZoomUserDetail zoomUserDetail =await _teleMedecineContext.ZoomUserDetails.Include(a => a.User).Where(a => a.User.Id == phcmaster.UserId).FirstOrDefaultAsync();
            if (zoomUserDetail!=null)
            {
                var responseModel = await CreateMeeting(zoomUserDetail.User.Email, zoomUserDetail.ZoomUserID);
                return responseModel;
            }

            return null;
        }
        public async Task<bool> DeleteMeeting(string meetingID)
        {

            bool responseModel = await _zoomMeetingService.DeleteMeeting(meetingID);
            return responseModel;
        }
        public async Task<bool> EndMeeting(string meetingID)
        {

            bool responseModel = await _zoomMeetingService.EndMeeting(meetingID);
            return responseModel;
        }

        public async Task<ZoomUserDetailDTO> CreateUser(string Email)
        {
            ZoomUserDetailDTO zoomUserDetailDTO = new ZoomUserDetailDTO();
            NewUserRequestModel NewUsermodel = new NewUserRequestModel();

            UserMaster userMaster = await _teleMedecineContext.UserMasters.Where(a => a.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            ZoomUserDetail zoomUserDetail = await _teleMedecineContext.ZoomUserDetails.Include(a => a.User).Where(a => a.User.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            if (userMaster == null)
            {
                return null;
            }
            else if (zoomUserDetail != null)
            {
                zoomUserDetailDTO.ID = zoomUserDetail.ID;
                zoomUserDetailDTO.UserId = zoomUserDetail.UserId;
                zoomUserDetailDTO.Account_number = zoomUserDetail.Account_number;
                zoomUserDetailDTO.account_id = zoomUserDetail.account_id;
                zoomUserDetailDTO.ZoomUserID = zoomUserDetail.ZoomUserID;
                zoomUserDetailDTO.Status = zoomUserDetail.Status;
            }
            else
            {
                var user = await _ZoomUserService.GetUser(Email);
                if (!string.IsNullOrEmpty(user.id))
                {
                    zoomUserDetailDTO.UserId = userMaster.Id;
                    zoomUserDetailDTO.Account_number = user.account_number.ToString();
                    zoomUserDetailDTO.account_id = user.account_id;
                    zoomUserDetailDTO.ZoomUserID = user.id;
                    zoomUserDetailDTO.Status = user.status;
                }
                else
                {
                    NewUsermodel.action = "create";
                    NewUsermodel.user_info.type = 2;
                    NewUsermodel.user_info.first_name = "";
                    NewUsermodel.user_info.last_name = "";
                    NewUsermodel.user_info.email = Email;
                    NewUsermodel.user_info.password = "Telemed@12345";
                    var newuser = await _ZoomUserService.CreateUser(NewUsermodel);

                    zoomUserDetailDTO.UserId = userMaster.Id;
                    zoomUserDetailDTO.Account_number = "";
                    zoomUserDetailDTO.account_id = "";
                    zoomUserDetailDTO.ZoomUserID = newuser.id;
                    zoomUserDetailDTO.Status = "pending";

                    zoomUserDetail = new ZoomUserDetail();
                    zoomUserDetail.UserId = userMaster.Id;
                    zoomUserDetail.Account_number = "";
                    zoomUserDetail.account_id = "";
                    zoomUserDetail.ZoomUserID = newuser.id;
                    zoomUserDetail.Status = "pending";

                    _teleMedecineContext.ZoomUserDetails.Add(zoomUserDetail);
                    await _teleMedecineContext.SaveChangesAsync();
                }
            }
            return zoomUserDetailDTO;
        }
        public async Task<ZoomUserDetailDTO> GetUserDetails(string Email)
        {
            ZoomUserDetailDTO zoomUserDetailDTO = new ZoomUserDetailDTO();
            UserMaster userMaster = await _teleMedecineContext.UserMasters.Where(a => a.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            ZoomUserDetail zoomUserDetail = await _teleMedecineContext.ZoomUserDetails.Include(a => a.User).Where(a => a.User.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            if (userMaster == null)
            {
                return null;
            }
            else if (zoomUserDetail != null)
            {
                zoomUserDetailDTO.ID = zoomUserDetail.ID;
                zoomUserDetailDTO.UserId = zoomUserDetail.UserId;
                zoomUserDetailDTO.Account_number = zoomUserDetail.Account_number;
                zoomUserDetailDTO.account_id = zoomUserDetail.account_id;
                zoomUserDetailDTO.ZoomUserID = zoomUserDetail.ZoomUserID;
                zoomUserDetailDTO.Status = zoomUserDetail.Status;
            }
            return zoomUserDetailDTO;
        }
        public async Task<ZoomUserDetailDTO> GetUserStatusFromZoom(string Email)
        {
            ZoomUserDetailDTO zoomUserDetailDTO = new ZoomUserDetailDTO();
            NewUserRequestModel NewUsermodel = new NewUserRequestModel();

            UserMaster userMaster = await _teleMedecineContext.UserMasters.Where(a => a.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            ZoomUserDetail zoomUserDetail = await _teleMedecineContext.ZoomUserDetails.Include(a => a.User).Where(a => a.User.Email.ToLower() == Email.ToLower()).FirstOrDefaultAsync();
            if (userMaster == null)
            {
                return null;
            }
            var user = await _ZoomUserService.GetUser(Email);
            if (zoomUserDetail != null && !string.IsNullOrEmpty(user.id))
            {
                zoomUserDetailDTO.UserId = userMaster.Id;
                zoomUserDetailDTO.Account_number = user.account_number.ToString();
                zoomUserDetailDTO.account_id = user.account_id;
                zoomUserDetailDTO.ZoomUserID = user.id;
                zoomUserDetailDTO.Status = user.status;

                zoomUserDetail.UserId = userMaster.Id;
                zoomUserDetail.Account_number = user.account_number.ToString();
                zoomUserDetail.account_id = user.account_id;
                zoomUserDetail.ZoomUserID = user.id;
                zoomUserDetail.Status = user.status;
                await _teleMedecineContext.SaveChangesAsync();
            }
            else if (zoomUserDetail == null && !string.IsNullOrEmpty(user.id))
            {
                zoomUserDetailDTO.UserId = userMaster.Id;
                zoomUserDetailDTO.Account_number = user.account_number.ToString();
                zoomUserDetailDTO.account_id = user.account_id;
                zoomUserDetailDTO.ZoomUserID = user.id;
                zoomUserDetailDTO.Status = user.status;

                zoomUserDetail = new ZoomUserDetail();
                zoomUserDetail.UserId = userMaster.Id;
                zoomUserDetail.Account_number = user.account_number.ToString();
                zoomUserDetail.account_id = user.account_id;
                zoomUserDetail.ZoomUserID = user.id;
                zoomUserDetail.Status = user.status;

                _teleMedecineContext.ZoomUserDetails.Add(zoomUserDetail);
                await _teleMedecineContext.SaveChangesAsync();
            }
            return zoomUserDetailDTO;
        }

        public async Task<bool> UpdateUserRecodingSetting(string ZoomUserID)
        {
            bool responseModel = await _ZoomUserService.UpdateRecodingSetting(ZoomUserID);
            return responseModel;
        }
    }
}
