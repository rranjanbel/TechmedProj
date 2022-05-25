using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class EquipmentUptimeReport
    {
        public int Id { get; set; }
        public int OtoScope { get; set; }
        public int Dermascope { get; set; }
        public int FetalDoppler { get; set; }
        public int HeadPhone { get; set; }
        public int Webcam { get; set; }
        public int Printer { get; set; }
        public int Inverter { get; set; }
        public int Computer { get; set; }
        public int PhcId { get; set; }
        public int Month { get; set; }
        public int Year { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }

        public virtual UserMaster? CreatedByNavigation { get; set; }
        public virtual Phcmaster Phc { get; set; } = null!;
        public virtual UserMaster? UpdatedByNavigation { get; set; }
    }
}
