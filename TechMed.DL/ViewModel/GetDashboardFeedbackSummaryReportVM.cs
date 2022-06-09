using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class GetDashboardFeedbackSummaryReportVM
    {
        public int? Feedback { get; set; }
        public int? FeedbackCount { get; set; }
    }
}
