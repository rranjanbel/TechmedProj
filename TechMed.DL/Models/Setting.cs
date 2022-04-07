using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class Setting
    {
        public int Id { get; set; }
        public long PatientNumber { get; set; }
        public long CaseFileNumber { get; set; }
    }
}
