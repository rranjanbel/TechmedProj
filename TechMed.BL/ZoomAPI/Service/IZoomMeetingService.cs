using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;
using static TechMed.BL.ZoomAPI.Model.NewMeetingRequest;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomMeetingService
    {
        Task<NewMeetingResponseModel> NewMeeting(NewMeetingRequestModel newMeetingRequestModel);
    }
}
