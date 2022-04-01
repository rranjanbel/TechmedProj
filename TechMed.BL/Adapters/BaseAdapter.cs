using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.Adapters
{
    public class BaseAdapter
    {
        public TeleMedecineContext teleMedecineContext;
        public BaseAdapter(TeleMedecineContext teleMedecineContext)
        {
            this.teleMedecineContext = teleMedecineContext;
        }
    }
}
