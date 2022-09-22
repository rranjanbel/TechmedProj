using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class ZoomUserDetail
    {
        public ZoomUserDetail()
        {
                      
        }

        public long ID { get; set; }
        public int UserId { get; set; }
        public string ZoomUserID { get; set; } = null!;
        public string? account_id { get; set; } = null!;
        public string? Account_number { get; set; } = null!;
        public string? Status { get; set; } = null!;

        public virtual UserMaster User { get; set; } = null!;
    }
}
