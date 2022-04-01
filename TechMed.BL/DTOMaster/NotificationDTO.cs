using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.DTOMaster
{
    public class NotificationDTO
    {
        public long Id { get; set; }
        public string FromUser { get; set; }
        public DateTime CreatedOn { get; set; }
        public string Message { get; set; } = null!;

    }
}
