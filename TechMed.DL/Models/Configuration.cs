using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class Configuration
    {
        public Configuration()
        {

        }

        public int ID { get; set; }
        public string? Name { get; set; }
        public string? Value { get; set; }
        public DateTime? UpdatedOn { get; set; }

    }
}
