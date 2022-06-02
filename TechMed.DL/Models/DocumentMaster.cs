using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class DocumentMaster
    {
        public DocumentMaster()
        {
            PatientCaseDocuments = new HashSet<PatientCaseDocument>();
        }

        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public virtual ICollection<PatientCaseDocument> PatientCaseDocuments { get; set; }
    }
}
