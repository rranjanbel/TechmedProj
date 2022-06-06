using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PrescribedMedicineVM
    {
        public long SrNo { get; set; }
        public string PrescribedMedicine { get; set; }
        public int NumberOfTimePrescribed { get; set; }
    }
}
