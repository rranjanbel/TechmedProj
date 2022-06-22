using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.Interfaces
{
    public interface ISnomedRepository
    {
        Task<List<SnomedCTCode>> SearchSnomedCodeByName(string searchText);
    }
}
