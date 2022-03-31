using System;
using System.Collections.Generic;

namespace TechMed.DL.Models
{
    public partial class FeedbackQuestionMaster
    {
        public int Id { get; set; }
        public string Question { get; set; } = null!;
        public bool? IsActive { get; set; }
    }
}
