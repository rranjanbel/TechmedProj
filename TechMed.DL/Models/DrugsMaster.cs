using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DrugsMaster
    {
        public DrugsMaster()
        {
            PatientCaseMedicines = new HashSet<PatientCaseMedicine>();
        }

        public int Id { get; set; }
        public string? DrugCode { get; set; }
        public string? GroupOfDrug { get; set; }
        public string? SubGroupOfDrug { get; set; }
        public string? MpaushidhiCode { get; set; }
        public string? NameOfDrug { get; set; }
        public string? DrugformAndStrength { get; set; }
        public string? PakAndVolume { get; set; }
        public string? Category { get; set; }
        public string? Reference { get; set; }
        public string? Remark { get; set; }
        public DateTime? CreatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual ICollection<PatientCaseMedicine> PatientCaseMedicines { get; set; }
    }
}
