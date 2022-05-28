using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class PatientCaseLevelDTO
    {
        public List<GetCaseLabelDTO> caseLabelDTOs { get; set; }
        public PatientMasterDTO patientMaster { get; set; }     
    }
}
