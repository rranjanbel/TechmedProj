using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<GetRecordingResponseModel>(myJsonResponse);
    public class GetRecordingResponseModel
    {
        public string account_id { get; set; }
        public int duration { get; set; }
        public string host_id { get; set; }
        public long id { get; set; }
        public int recording_count { get; set; }
        public DateTime start_time { get; set; }
        public string topic { get; set; }
        public int total_size { get; set; }
        public string type { get; set; }
        public string uuid { get; set; }
        public List<RecordingFile> recording_files { get; set; }
        public string download_access_token { get; set; }
        public string password { get; set; }
        public string recording_play_passcode { get; set; }
        public List<ParticipantAudioFile> participant_audio_files { get; set; }
    }
    public class ParticipantAudioFile
    {
        public string download_url { get; set; }
        public string file_name { get; set; }
        public string file_path { get; set; }
        public int file_size { get; set; }
        public string file_type { get; set; }
        public string id { get; set; }
        public string play_url { get; set; }
        public DateTime recording_end { get; set; }
        public DateTime recording_start { get; set; }
        public string status { get; set; }
    }

    public class RecordingFile
    {
        public DateTime deleted_time { get; set; }
        public string download_url { get; set; }
        public string file_path { get; set; }
        public string file_size { get; set; }
        public string file_type { get; set; }
        public string id { get; set; }
        public string meeting_id { get; set; }
        public string play_url { get; set; }
        public DateTime recording_end { get; set; }
        public DateTime recording_start { get; set; }
        public string recording_type { get; set; }
        public string status { get; set; }
    }

   


}
