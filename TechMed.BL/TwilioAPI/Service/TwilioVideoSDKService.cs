using TechMed.BL.TwilioAPI.Model;
using Twilio;
using Twilio.Base;
using Twilio.Jwt.AccessToken;
using Twilio.Rest.Video.V1;
using Twilio.Rest.Video.V1.Room;
using ParticipantStatus = Twilio.Rest.Video.V1.Room.ParticipantResource.StatusEnum;
using static Twilio.Rest.Video.V1.CompositionResource;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using TechMed.BL.Repository.Interfaces;

namespace TechMed.BL.TwilioAPI.Service
{
    public class TwilioVideoSDKService : ITwilioVideoSDKService
    {
        readonly TwilioSettings _twilioSettings;
        public TwilioVideoSDKService(Microsoft.Extensions.Options.IOptions<TwilioSettings> twilioOptions)
        {
            _twilioSettings =
                twilioOptions?.Value
             ?? throw new ArgumentNullException(nameof(twilioOptions));

            TwilioClient.Init(_twilioSettings.ApiKey, _twilioSettings.ApiSecret);
        }

        public string GetTwilioJwt(string identity)
        {
            return new Token(_twilioSettings.AccountSid,
                            _twilioSettings.ApiKey,
                            _twilioSettings.ApiSecret,
                            identity ?? Guid.NewGuid().ToString(),
                            grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();
        }
        public async Task<IEnumerable<RoomDetails>> GetAllRoomsAsync()
        {
            var rooms = await RoomResource.ReadAsync();
            var tasks = rooms.Select(
                room => GetRoomDetailsAsync(
                    room,
                    ParticipantResource.ReadAsync(
                        room.Sid,
                        ParticipantStatus.Connected)));

            return await Task.WhenAll(tasks);

            static async Task<RoomDetails> GetRoomDetailsAsync(
                RoomResource room,
                Task<ResourceSet<ParticipantResource>> participantTask)
            {
                var participants = await participantTask;
                return new(
                    room.Sid,
                    room.UniqueName,
                    participants.Count(),
                    room.MaxParticipants ?? 0);

            }
        }


        public async Task<RoomResource> CreateRoomsAsync(string roomname, string callBackUrl)
        {
            var room = await RoomResource.CreateAsync(
            recordParticipantsOnConnect: true,
            statusCallbackMethod: Twilio.Http.HttpMethod.Post,
            emptyRoomTimeout:1,
            unusedRoomTimeout: 1,
            
            //emptyRoomTimeout: 10,
            statusCallback: new Uri(callBackUrl),
            type: RoomResource.RoomTypeEnum.GroupSmall,
            maxParticipants: 2,
            uniqueName: roomname
//            videoCodecs: new List<RoomResource.VideoCodecEnum>() { RoomResource.VideoCodecEnum.H264}
            );


            return room;
        }
        public async Task<RoomResource> CloseRoomAsync(string roomname)
        {
            return await RoomResource.UpdateAsync(new UpdateRoomOptions(roomname,RoomResource.RoomStatusEnum.Completed));
        }
        public async Task<ResourceSet<CompositionResource>> GetAllCompletedComposition()
        {
            var composition = await CompositionResource.ReadAsync(status: CompositionResource.StatusEnum.Completed);
            if (composition != null)
            {
                foreach (CompositionResource resource in composition)
                {

                }
                return composition;
            }

            else
                return null;
        }
        public async Task<List<RoomResource>> GetAllCompletedCall()
        {
            List<RoomResource> roomResources = new List<RoomResource>();
            var rooms = await RoomResource.ReadAsync(status: RoomResource.RoomStatusEnum.Completed);
            var composition = await CompositionResource.ReadAsync(status: CompositionResource.StatusEnum.Completed);
            List<RoomResource> listRoomResource = rooms.ToList();
            List<CompositionResource> listCompositionResources = composition.ToList();
            roomResources = listRoomResource.ExceptBy(listCompositionResources.Select(comp => comp.RoomSid), room => room.Sid).ToList();

            if (roomResources != null && roomResources.Count > 0)
                return roomResources;
            else
                return null;
        }
        public async Task<CompositionResource> ComposeVideo(string roomSid, string callBackUrl)
        {
            //var roomDetail = RoomResource.Fetch(roomName);
            var layout = new
            {
                transcode = new
                {
                    video_sources = new string[] { "*" }
                }
            };
            var composition = await CompositionResource.CreateAsync(
                                  roomSid: roomSid,
                                  audioSources: new List<string> { "*" },
                                  videoLayout: layout,
                                  statusCallback: new Uri(callBackUrl),
                                  format: FormatEnum.Mp4
                              );

            return composition;

        }
        public async Task<string> DownloadComposeVideo(string compositionSid)
        {
            string uri = "https://video.twilio.com/v1/Compositions/" + compositionSid + "/Media?Ttl=3600";

            var request = (HttpWebRequest)WebRequest.Create(uri);
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(_twilioSettings.ApiKey + ":" + _twilioSettings.ApiSecret)));
            request.AllowAutoRedirect = false;
            string responseBody = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            var mediaLocation = await
                Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody)["redirect_to"]);
            return responseBody;

            //new WebClient().DownloadFile(mediaLocation, $"{compositionSid}.out");

        }
        public async Task<bool> DeleteComposeVideo(string compositionSid)
        {
            return await CompositionResource.DeleteAsync(compositionSid);
        }
        public async Task<string> GetRoomSid(string roomName)
        {
            string roomSid = null;
            if (!string.IsNullOrEmpty(roomName))
            {
                var room = await RoomResource.FetchAsync(pathSid: roomName);
                roomSid = room.Sid;
            }
            return roomSid;
        }
        public async Task<RoomResource> EndVideoCall(string roomName)
        {
            var roomDetail = RoomResource.Fetch(roomName);
            var room = await RoomResource.UpdateAsync(
                       status: RoomResource.RoomStatusEnum.Completed,
                       pathSid: roomDetail.Sid);
            return room;
        }

        public async Task<RoomResource> GetRoomDetailFromTwilio(string roomName)
        {
            try
            {
                return await RoomResource.FetchAsync(new FetchRoomOptions(roomName));

            }
            catch (Exception)
            {
                return null;
            }

        }

        public async Task<List<ParticipantResource>> GetConnectedParticipientFromTwilio(string roomName)
        {
            ResourceSet<ParticipantResource> participants = await ParticipantResource.ReadAsync(roomName, ParticipantResource.StatusEnum.Connected);
            return participants.ToList();
        }

        //public async Task<bool> FirebaseRegisterTokenToDb(string UserName, string Token)
        //{

        //    return true;
        //}


        #region Borrowed from https://github.com/twilio/video-quickstart-js/blob/1.x/server/randomname.js

        readonly string[] _adjectives =
        {
            "Abrasive", "Brash", "Callous", "Daft", "Eccentric", "Feisty", "Golden",
            "Holy", "Ignominious", "Luscious", "Mushy", "Nasty",
            "OldSchool", "Pompous", "Quiet", "Rowdy", "Sneaky", "Tawdry",
            "Unique", "Vivacious", "Wicked", "Xenophobic", "Yawning", "Zesty"
        };

        readonly string[] _firstNames =
        {
            "Anna", "Bobby", "Cameron", "Danny", "Emmett", "Frida", "Gracie", "Hannah",
            "Isaac", "Jenova", "Kendra", "Lando", "Mufasa", "Nate", "Owen", "Penny",
            "Quincy", "Roddy", "Samantha", "Tammy", "Ulysses", "Victoria", "Wendy",
            "Xander", "Yolanda", "Zelda"
        };

        readonly string[] _lastNames =
        {
            "Anchorage", "Berlin", "Cucamonga", "Davenport", "Essex", "Fresno",
            "Gunsight", "Hanover", "Indianapolis", "Jamestown", "Kane", "Liberty",
            "Minneapolis", "Nevis", "Oakland", "Portland", "Quantico", "Raleigh",
            "SaintPaul", "Tulsa", "Utica", "Vail", "Warsaw", "XiaoJin", "Yale",
            "Zimmerman"
        };



        string GetName() => $"{_adjectives.RandomElement()} {_firstNames.RandomElement()} {_lastNames.RandomElement()}";

        #endregion
    }
    
}

