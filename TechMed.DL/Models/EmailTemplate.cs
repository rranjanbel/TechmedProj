using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.DL.Models
{
    public partial class EmailTemplate
    {
        public int ID { get; set; }
        public string? Subject { get; set; }
        public string? Body { get; set; }
        public int UsertTypeID { get; set; }
        public string? ApplicationURL { get; set; }

        public virtual UserTypeMaster UserType { get; set; } = null!;
    }
}
