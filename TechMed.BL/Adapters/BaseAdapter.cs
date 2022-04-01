using AutoMapper;
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
        public readonly IMapper mapper;
        public BaseAdapter(TeleMedecineContext teleMedecineContext, IMapper mapper)
        {
            this.teleMedecineContext = teleMedecineContext;
           this.mapper=mapper;
        }
    }
}
