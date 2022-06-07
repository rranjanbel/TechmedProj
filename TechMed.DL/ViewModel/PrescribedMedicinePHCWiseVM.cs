using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.ViewModel
{
    [Keyless]
    public class PrescribedMedicinePHCWiseVM
    {
        public long SrNo { get; set; }
        public string DistrictName { get; set; }
        public string BlockName { get; set; }
        public string PHCName { get; set; }
        public string PrescribedMedicine { get; set; }
        public int NumberOfTimePrescribed { get; set; }
        public int EAushadhiStock { get; set; }
        public int QuantityPrescribed { get; set; }
        
    }
}
