using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class AgeGroupMaster
    {
        //[ID][int] IDENTITY(1,1) NOT NULL,

        //[AgeMaxLimit] [int] NOT NULL,

        //[AgeMinLimit] [int] NOT NULL,

        //[AgeRange] [nvarchar] (50) NOT NULL,

        //[GenderID] [int] NOT NULL,

        //[SpecializationID] [int] NOT NULL,      
        public int ID { get; set; }
        public int AgeMaxLimit { get; set; }
        public int AgeMinLimit { get; set; }
        public string AgeRange { get; set; }
        public int GenderID { get; set; }
        public int SpecializationID { get; set; }
        public int DaysOrYear { get; set; } 

        public virtual GenderMaster Gender { get; set; } = null!;
        public virtual SpecializationMaster Specialization { get; set; } = null!;

    }
}
