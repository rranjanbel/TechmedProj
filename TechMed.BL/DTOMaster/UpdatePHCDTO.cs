using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class UpdatePHCDTO
    {
        public int Id { get; set; }
        public int ClusterId { get; set; }
        public int DivisionId { get; set; }
        public int DistrictId { get; set; }
        public int BlockId { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public int? UpdatedBy { get; set; }
        public string? EmployeeName { get; set; }

        //public string Phcname { get; set; } = null!;
        //public string MailId { get; set; } = null!;
        //public string? Address { get; set; }
    }
}
