using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class InsertIntoTwilioVideoDownloadStatusVM
    {
        public int Success { get; set; }
    }
}
