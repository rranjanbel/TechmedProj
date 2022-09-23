using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public class ZoomToken
    {
        public int ID { get; set; }
        public int? ActiveTokenNumber { get; set; }
        public DateTime Token1CreatedOn { get; set; }
        public string? Token1 { get; set; }
        public DateTime Token2CreatedOn { get; set; }
        public string? Token2 { get; set; }

    }
}
