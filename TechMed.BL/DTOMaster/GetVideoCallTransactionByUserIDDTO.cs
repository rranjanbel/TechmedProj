using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class GetVideoCallTransactionByUserIDDTO
    {
        public long Id { get; set; }
        public int FromUserID { get; set; }
        public string FromUserName { get; set; }
        public int PatientId { get; set; }
        public string PatientFName { get; set; }
        public string PatientMName { get; set; }
        public string PatientLName { get; set; }
        public string RoomId { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
