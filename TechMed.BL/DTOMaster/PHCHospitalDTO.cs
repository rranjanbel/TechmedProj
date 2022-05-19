using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PHCHospitalDTO
    {
        public int Id { get; set; }
        public int BlockId { get; set; }
        public int ClusterId { get; set; }
        public int UserId { get; set; }
        public string Phcname { get; set; } = null!;
        public string MailId { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public string? Address { get; set; }
        public int CreatedBy { get; set; }
        public int ModifyBy { get; set; }
    }
}
