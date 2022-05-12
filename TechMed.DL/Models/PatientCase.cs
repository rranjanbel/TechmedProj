﻿using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class PatientCase
    {
        public PatientCase()
        {
            PatientCaseDocuments = new HashSet<PatientCaseDocument>();
            PatientCaseFeedbacks = new HashSet<PatientCaseFeedback>();
            PatientCaseMedicines = new HashSet<PatientCaseMedicine>();
            PatientCaseVitals = new HashSet<PatientCaseVital>();
            PatientQueues = new HashSet<PatientQueue>();
            VideoCallTransactions = new HashSet<VideoCallTransaction>();
        }

        public long Id { get; set; }
        public int PatientId { get; set; }
        public string CaseFileNumber { get; set; } = null!;
        public string CaseHeading { get; set; } = null!;
        public int SpecializationId { get; set; }
        public string? Symptom { get; set; }
        public string? Observation { get; set; }
        public string? Allergies { get; set; }
        public string? FamilyHistory { get; set; }
        public string? Diagnosis { get; set; }
        public string? Instruction { get; set; }
        public string? Test { get; set; }
        public string? Finding { get; set; }
        public string? Prescription { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }

        public virtual UserMaster CreatedByNavigation { get; set; } = null!;
        public virtual PatientMaster Patient { get; set; } = null!;
        public virtual SpecializationMaster Specialization { get; set; } = null!;
        public virtual UserMaster? UpdatedByNavigation { get; set; }
        public virtual ICollection<PatientCaseDocument> PatientCaseDocuments { get; set; }
        public virtual ICollection<PatientCaseFeedback> PatientCaseFeedbacks { get; set; }
        public virtual ICollection<PatientCaseMedicine> PatientCaseMedicines { get; set; }
        public virtual ICollection<PatientCaseVital> PatientCaseVitals { get; set; }
        public virtual ICollection<PatientQueue> PatientQueues { get; set; }
        public virtual ICollection<VideoCallTransaction> VideoCallTransactions { get; set; }
    }
}
