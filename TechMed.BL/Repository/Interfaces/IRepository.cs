using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TechMed.BL.Repository.Interfaces
{
    public interface IRepository<T> : IDisposable where T : class
    {       

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <returns>The model list.</returns>
        Task<IEnumerable<T>> Get();

        Task<List<T>> GetAll();

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <returns>The model</returns>
        Task<T> Get(int id);

        /// <summary>
        /// Gets the specified where.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>The model</returns>
        Task<IEnumerable<T>> Get(Func<T, bool> where);

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The task</returns>
        Task<T> Create(T model);

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The task</returns>
        Task<T> Update(T model);

        /// <summary>
        /// Deletes the model.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <returns>Task</returns>
        Task Delete(int id);

        Task<T> UpdateOnly(T model);
    }
}
