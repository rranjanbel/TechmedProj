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
        public string ZoneName { get; set; } = null!;
        public string ClusterName { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public string? Phcname { get; set; }
        public string MailId { get; set; } = null!;
        public string PhoneNo { get; set; } = null!;
        public string Moname { get; set; } = null!;
        public string? Address { get; set; }       
    }
}
