using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.ZoomAPI.Model;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomRecordingService
    {
        Task<GetRecordingResponseModel> GetRecording(string MeetingID);
    }
}
