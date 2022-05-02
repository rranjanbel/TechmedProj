using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.DTOMaster;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IVideoCallTransactionRespository
    {
        public  Task<List<GetVideoCallTransactionByUserIDDTO>> GetVideoCallTransactionByUserID(int UserID);
        public Task<VideoCallTransactionDTO> PostVideoCallTransaction(VideoCallTransactionDTO videoCallTransactionDTO);
    }
}
