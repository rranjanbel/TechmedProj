using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ViewModels
{
    public class ApiResponseModel<T>
    {
        public bool isSuccess { get; set; }
        public T data { get; set; }
        public string meetingID { get; set; }
        public string meetingStartURL { get; set; }
        public string errorMessage { get; set; }
    }
}
