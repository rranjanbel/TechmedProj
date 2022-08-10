using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class SearchPHCDetailsIdsVM
    {
        public int PHCId { get; set; }
        public string? Phcname { get; set; }
        public int? DivisionID { get; set; }
        public string? Division { get; set; }
        public int? DistrictID { get; set; }
        public string? District { get; set; }
        public int BLockID { get; set; }
        public string BLockName { get; set; } = null!;
        public int ClusterId { get; set; }
        public string ClusterName { get; set; } = null!;
        public string? Address { get; set; }
        public string? City { get; set; }
        public int? StateID { get; set; }
        public string? State { get; set; }
        public string? PinCode { get; set; }

        public string? EmployeeName { get; set; }

        public string? MailId { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string? LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;
        public string? Moname { get; set; } = null!;
    }
}
