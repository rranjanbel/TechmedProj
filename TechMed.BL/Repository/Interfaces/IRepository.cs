using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {
        IQueryable<T> Get();

        T Get(int id);

        IQueryable<T> Add(T entity);

        int Delete(int id);

        IQueryable<T> Update(T entity);
        int Deactivate(int id);
    }
}
