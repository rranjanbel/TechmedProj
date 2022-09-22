using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Model
{

    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class UpdateUserRecordingSettingModel
    {
        public UpdateUserRecordingSettingModel()
        {
            recording = new Recording();
        }
        public Recording recording { get; set; }
    }

    public class Recording
    {
        public bool account_user_access_recording { get; set; } = true;
        public bool allow_recovery_deleted_cloud_recordings { get; set; } = true;
        public bool auto_delete_cmr { get; set; } = true;
        public int auto_delete_cmr_days { get; set; } = 90;
        public bool display_participant_name { get; set; } = true;
        public bool recording_thumbnails { get; set; } = true;
        public bool recording_highlight { get; set; } = true;
        public string auto_recording { get; set; } = "cloud";
        public bool cloud_recording { get; set; } = true;
        public bool cloud_recording_download { get; set; } = true;
        public bool cloud_recording_download_host { get; set; } = true;
        public bool host_delete_cloud_recording { get; set; } = true;
        public bool local_recording { get; set; } = true;
        public bool prevent_host_access_recording { get; set; } = true;
        public bool record_audio_file { get; set; } = true;
        public bool record_gallery_view { get; set; } = true;
        public bool record_speaker_view { get; set; } = true;
        public bool recording_audio_transcript { get; set; } = true;
        public bool recording_disclaimer { get; set; } = true;
        public bool show_timestamp { get; set; } = true;                    
    }




}
