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

namespace TechMed.BL.TwilioAPI.Service
{
    public class VideoService : IVideoService
    {
        readonly TwilioSettings _twilioSettings;

        public VideoService(Microsoft.Extensions.Options.IOptions<TwilioSettings> twilioOptions)
        {
            _twilioSettings =
                twilioOptions?.Value
             ?? throw new ArgumentNullException(nameof(twilioOptions));

            TwilioClient.Init(_twilioSettings.ApiKey, _twilioSettings.ApiSecret);
        }

        public string GetTwilioJwt(string identity)
            => new Token(_twilioSettings.AccountSid,
                         _twilioSettings.ApiKey,
                         _twilioSettings.ApiSecret,
                         identity ?? Guid.NewGuid().ToString(),
                         grants: new HashSet<IGrant> { new VideoGrant() }).ToJwt();

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
           var room =await RoomResource.CreateAsync(
           recordParticipantsOnConnect: true,
           statusCallback: new Uri(callBackUrl),
           type: RoomResource.RoomTypeEnum.GroupSmall,
           maxParticipants:2,
           uniqueName: roomname
           );

            return room;
        }
        public async Task<ResourceSet<CompositionResource>> GetAllCompletedComposition()
        {
            var composition = await CompositionResource.ReadAsync(status: CompositionResource.StatusEnum.Completed);
            if (composition != null)
                return composition;
            else
                return null;
        }      
        public async Task<CompositionResource> ComposeVideo(string roomSid, string callBackUrl)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
            //var roomDetail = RoomResource.Fetch(roomName);
            var layout = new {
                transcode = new
                {
                    video_sources = new string[] { "*" }
                }
            };
            var composition = await CompositionResource.CreateAsync(
                                  roomSid: roomSid,
                                  audioSources: new List<string>{"*"},
                                  videoLayout: layout,
                                  statusCallback: new Uri(callBackUrl),
                                  format: FormatEnum.Mp4
                              );

            return composition;

        }
        public async Task<string> DownloadComposeVideo(string compositionSid)
        {
            string uri = "https://video.twilio.com/v1/Compositions/"+ compositionSid + "/Media?Ttl=3600";

            var request = (HttpWebRequest) WebRequest.Create(uri);
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes(_twilioSettings.ApiKey + ":" + _twilioSettings.ApiSecret)));
            request.AllowAutoRedirect = false;
            string responseBody = new StreamReader(request.GetResponse().GetResponseStream()).ReadToEnd();
            var mediaLocation = await
                Task.Factory.StartNew(() => JsonConvert.DeserializeObject<Dictionary<string, string>>(responseBody)["redirect_to"]);
            return mediaLocation.ToString();

            //new WebClient().DownloadFile(mediaLocation, $"{compositionSid}.out");

        }
        public async Task<bool> DeleteComposeVideo(string compositionSid)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
            return  await CompositionResource.DeleteAsync(compositionSid);
        }
        public async Task<string> GetRoomSid(string roomName)
        {
            TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);
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
    static class StringArrayExtensions
    {
        static readonly Random Random = new((int)DateTime.Now.Ticks);

        internal static string RandomElement(this IReadOnlyList<string> array)
            => array[Random.Next(array.Count)];
    }
}

