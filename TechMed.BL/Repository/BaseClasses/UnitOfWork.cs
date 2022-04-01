using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;

namespace TechMed.BL.Repository.BaseClasses
{
    public class UnitOfWork : IUnitOfWork
    {
        public DbContext Context { get; }

        public UnitOfWork(DbContext context)
        {
            Context = context;
        }

        public int Commit()
        {
            int value = Context.SaveChanges();
            return value;
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
