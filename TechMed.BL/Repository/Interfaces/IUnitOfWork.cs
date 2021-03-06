using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        DbContext Context { get; }
        int Commit();
    }
}
