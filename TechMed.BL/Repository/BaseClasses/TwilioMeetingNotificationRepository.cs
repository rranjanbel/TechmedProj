﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.CommanClassesAndFunctions;
using TechMed.BL.DTOMaster;
using TechMed.BL.Repository.Interfaces;
using TechMed.BL.ViewModels;
using TechMed.DL.Models;
using TechMed.DL.ViewModel;
using Twilio.Rest.Video.V1;

namespace TechMed.BL.Repository.BaseClasses
{
    public class TwilioMeetingRepository : Repository<TwilioMeetingRoomInfo>, ITwilioMeetingRepository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<TwilioMeetingRepository> _logger;
        public TwilioMeetingRepository(ILogger<TwilioMeetingRepository> logger,
            TeleMedecineContext teleMedecineContext, IMapper mapper) : base(teleMedecineContext)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }

        public async Task<TwilioMeetingRoomInfo> MeetingRoomInfoGet(string RoomName)
        {
            return await _teleMedecineContext.TwilioMeetingRoomInfos.FirstOrDefaultAsync(x => x.RoomName == RoomName);

        }
        public async Task<bool> MeetingRoomInfoAdd(TwilioMeetingRoomInfo doctorMeetingRoomInfo)
        {
            _teleMedecineContext.TwilioMeetingRoomInfos.Add(doctorMeetingRoomInfo);
            _teleMedecineContext.Entry(doctorMeetingRoomInfo).State = EntityState.Added;
            return (await _teleMedecineContext.SaveChangesAsync()) > 0;
        }

        public async Task<PatientQueue> PatientQueueGet(int patientCaseID)
        {
            return await _teleMedecineContext.PatientQueues
                .Include(x => x.PatientCase)
                .Include(x => x.AssignedByNavigation)
                .ThenInclude(x => x.User).FirstOrDefaultAsync(x => x.PatientCaseId == patientCaseID);
        }

        public async Task<bool> MeetingRoomCloseFlagUpdate(long ID, bool isClosed)
        {

            var meetInfo = await _teleMedecineContext.TwilioMeetingRoomInfos.FirstOrDefaultAsync(x => x.ID == ID);
            if (meetInfo != null)
            {
                meetInfo.IsClosed = isClosed;
                _teleMedecineContext.TwilioMeetingRoomInfos.Add(meetInfo);
                _teleMedecineContext.Entry(meetInfo).State = EntityState.Modified;
                return (await _teleMedecineContext.SaveChangesAsync()) > 0;
            }
            else
            {
                return true;
            }
        }
        public async Task<bool> SetMeetingRoomClosed(string roomName)
        {

            var meetInfo = await _teleMedecineContext.TwilioMeetingRoomInfos.FirstOrDefaultAsync(x => x.RoomName == roomName);
            if (meetInfo != null)
            {
                meetInfo.IsClosed = true;
                meetInfo.CloseDate = DateTime.Now;
                //meetInfo.TwilioRoomStatus = "Closed";
                _teleMedecineContext.TwilioMeetingRoomInfos.Add(meetInfo);
                _teleMedecineContext.Entry(meetInfo).State = EntityState.Modified;
                return (await _teleMedecineContext.SaveChangesAsync()) > 0;
            }
            else
            {
                return true;
            }
        }

        public async Task<bool> MeetingRoomComposeVideoUpdate(CompositionResource compositionResource, string roomName)
        {

            var meetInfo = await _teleMedecineContext.TwilioMeetingRoomInfos.FirstOrDefaultAsync(x => x.RoomName == roomName);
            if (meetInfo != null)
            {
                meetInfo.IsClosed = true;
                meetInfo.CompositeVideoSID = compositionResource.Sid;
                meetInfo.CompositeVideoSize = compositionResource.Size;
                _teleMedecineContext.TwilioMeetingRoomInfos.Add(meetInfo);
                _teleMedecineContext.Entry(meetInfo).State = EntityState.Modified;
                return (await _teleMedecineContext.SaveChangesAsync()) > 0;
            }
            else
            {
                return true;
            }
        }
    }
}