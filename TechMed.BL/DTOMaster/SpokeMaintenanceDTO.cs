using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class SpokeMaintenanceDTO
    {
        public int Phcid { get; set; }      
        public IFormFile file { get; set; }
        public DateTime dateTime { get; set; }
    }
}
