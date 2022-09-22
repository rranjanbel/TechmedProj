using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster.Zoom
{
    public class ZoomUserDetailDTO
    {
        public long ID { get; set; }
        public int UserId { get; set; }
        public string ZoomUserID { get; set; } = null!;
        public string? account_id { get; set; } = null!;
        public string? Account_number { get; set; } = null!;
        public string? Status { get; set; } = null!;
    }
}
