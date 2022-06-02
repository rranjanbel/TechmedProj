﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using Twilio.Rest.Video.V1;

namespace TechMed.BL.Repository.Interfaces
{
    public interface ITwilioMeetingRepository : IRepository<TwilioMeetingRoomInfo>
    {
        Task<TwilioMeetingRoomInfo> MeetingRoomInfoGet(string RoomName);
        Task<bool> MeetingRoomInfoAdd(TwilioMeetingRoomInfo doctorMeetingRoomInfo);
        Task<PatientQueue> PatientQueueGet(int patientCaseID);
        Task<bool> MeetingRoomCloseFlagUpdate(long ID, bool isClosed);
        Task<bool> SetMeetingRoomClosed(string roomName);
        Task<bool> MeetingRoomComposeVideoUpdate(CompositionResource compositionResource, string roomName);
    }
}