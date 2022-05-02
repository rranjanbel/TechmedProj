using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    [Keyless]
    public class AdvanceSearchPatientVM
    {
        //@PHCID int ,
        //@PatientName varchar(50) = NULL,
        //@PatientUID bigint,
        //@DateOfRegistration Date,
        //@ContactNo varchar(10) = NULL,
        //@DateOfBirth Date,
        //@GenderId int

        public int PHCID { get; set; }
        public string PatientName { get; set; }
        public long PatientUID { get; set; }
        //public DateTime? DateOfRegistration { get; set; }
        public string ContactNo { get; set; }
        //public DateTime? DateOfBirth { get; set; }
        public int GenderId { get; set; }
    }
}
