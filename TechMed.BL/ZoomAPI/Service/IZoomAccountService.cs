using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.ZoomAPI.Service
{
    public interface IZoomAccountService
    {
        Task<string> GetNewTokenFromZoomAsync(int index = 0);
        Task<string> GetIssuedTokenAsync();
        Task<bool> RotateTokenAsync();
    }
}
