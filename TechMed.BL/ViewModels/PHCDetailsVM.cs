using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class PHCDetailsVM
    {
        public int Id { get; set; }
        public string? Phcname { get; set; }
        public string ZoneName { get; set; } = null!;
        public string ClusterName { get; set; } = null!;
        public string? Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }       
        public string PinCode { get; set; }

        public string MailId { get; set; }
        public string PhoneNo { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string Gender { get; set; } = null!;       
        public string Moname { get; set; } = null!;
              
    }
}
