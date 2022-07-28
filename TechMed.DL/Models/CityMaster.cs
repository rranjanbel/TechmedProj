using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class CityMaster
    {
        public CityMaster()
        {
            
        }

        public Int64 ID { get; set; }
        public string? CityName { get; set; }

    }
}
