using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace TechMed.DL.ViewModel
{
    public class GetListOfNotificationVM
    {
        [Required]
        public string UserEmail { get; set; }
    }
}
