using AutoMapper;
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
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class VideoCallTransactionRespository : IVideoCallTransactionRespository
    {
        private readonly TeleMedecineContext _teleMedecineContext;
        private readonly IMapper _mapper;
        private readonly ILogger<VideoCallTransactionRespository> _logger;
        public VideoCallTransactionRespository(ILogger<VideoCallTransactionRespository> logger, TeleMedecineContext teleMedecineContext, IMapper mapper)
        {
            this._teleMedecineContext = teleMedecineContext;
            this._mapper = mapper;
            this._logger = logger;
        }
        public async Task<List<GetVideoCallTransactionByUserIDDTO>> GetVideoCallTransactionByUserID(int UserID)
        {
            List<GetVideoCallTransactionByUserIDDTO> getVideoCallTransactionByUserIDDTOs = new List<GetVideoCallTransactionByUserIDDTO>();
            List<VideoCallTransaction> VideoCallTransactions = await _teleMedecineContext.VideoCallTransactions
                .Include(a => a.FromUser)
                .Include(a => a.Patient)
                .Where(a => a.ToUserId == UserID).ToListAsync();
            foreach (var item in VideoCallTransactions)
            {
                getVideoCallTransactionByUserIDDTOs.Add(

                    new GetVideoCallTransactionByUserIDDTO
                    {
                        //CreatedOn = item.CreatedOn,
                        FromUserID = item.FromUserId,
                        FromUserName = item.FromUser.Name,
                        Id = item.Id,
                        PatientId = item.PatientId,
                        PatientFName = item.Patient.FirstName,
                        PatientMName = "",
                        PatientLName = item.Patient.LastName,
                        RoomId = item.RoomId,
                    });
            }

            return getVideoCallTransactionByUserIDDTOs;
        }
        public async Task<VideoCallTransactionDTO> PostVideoCallTransaction(VideoCallTransactionDTO videoCallTransactionDTO)
        {
            VideoCallTransaction videoCallTransaction = new VideoCallTransaction()
            {
                //CreatedOn = DateTime.Now,
                CreatedOn = UtilityMaster.GetLocalDateTime(),
                FromUserId = videoCallTransactionDTO.FromUserId,
                ToUserId = videoCallTransactionDTO.ToUserId,
                PatientId = videoCallTransactionDTO.PatientId,
                RecordingLink = videoCallTransactionDTO.RecordingLink,
                RoomId = videoCallTransactionDTO.RoomId,
            };
            await _teleMedecineContext.VideoCallTransactions.AddAsync(videoCallTransaction);
            videoCallTransactionDTO.Id = videoCallTransaction.Id;
            int count = await _teleMedecineContext.SaveChangesAsync();
            if (count > 0)
            {
                return videoCallTransactionDTO;
            }
            return null;
        }
    }
}
