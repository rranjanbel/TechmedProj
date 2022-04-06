using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TechMed.BL.Repository.Interfaces;
using TechMed.DL.Models;

namespace TechMed.BL.Repository.BaseClasses
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected TeleMedecineContext Context { get; set; }

        public Repository(TeleMedecineContext context)
        {
            //Context = context;
            this.Context = context ?? throw new ArgumentNullException("Context");
        }

        /// <summary>
        /// Gets the model list.
        /// </summary>
        /// <returns>
        /// The model list.
        /// </returns>
        public async Task<IEnumerable<T>> Get()
        {
            var entities = await this.Context.Set<T>().ToListAsync();
            return entities;
        }
        public async Task<List<T>> GetAll()
        {
            var entities = await this.Context.Set<T>().ToListAsync();
            return entities;
        }

        /// <summary>
        /// Gets the model.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <returns>
        /// The model
        /// </returns>
        //public async Task<T> Get(int id)
        //{
        //    var entity = await this.Context.Set<T>().SingleOrDefaultAsync(e => e.ID == id);
        //    return entity;
        //    //return Context.Set<T>().FirstOrDefault(GetDynamicExpresion("ID", id));
        //    //var entity = await this.Context.Set<T>().SingleOrDefaultAsync(GetDynamicExpresion("ID", id));
        //    //return entity;


        //}

        /// <summary>
        /// Gets the many.
        /// </summary>
        /// <param name="where">The where.</param>
        /// <returns>
        /// Entity
        /// </returns>
        public async Task<IEnumerable<T>> Get(Func<T, bool> where)
        {
            var entities = await Task.Run(() => this.Context.Set<T>().Where(where).AsEnumerable());
            return entities;
        }

        /// <summary>
        /// Creates the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>The task</returns>
        public async Task<T> Create(T model)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }

            //model.IsDeleted = false;
            //if (model.Created == DateTimeOffset.MinValue)
            //{
            //    model.Created = DateTimeOffset.UtcNow;
            //}

            try
            {
                this.Context.Add(model);

                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return model;
        }

        /// <summary>
        /// Updates the model.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <returns>
        /// The task
        /// </returns>
        public async Task<T> Update(T model)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }

            /*model.Updated = DateTimeOffset.UtcNow*/;

            try
            {
                this.Context.Entry(model).State = EntityState.Modified;
                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return model;
        }

        /// <summary>
        /// Deletes the model.
        /// </summary>
        /// <param name="id">The model identifier.</param>
        /// <returns>
        /// The task
        /// </returns>
        public async Task Delete(int id)
        {
            if (id < 1)
            {
                throw new ArgumentOutOfRangeException("id");
            }

            var entity = await this.Get(id);
            //entity.IsDeleted = true;

            this.Context.Entry(entity).State = EntityState.Modified;
            await this.Context.SaveChangesAsync();
        }

        public async Task<T> UpdateOnly(T model)
        {
            if (model == null)
            {
                throw new ArgumentException("model");
            }

            //model.UpdateBy = DateTimeOffset.UtcNow;

            try
            {
                this.Context.Entry(model).State = EntityState.Modified;
                await this.Context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                throw;
            }

            return model;
        }

        public void Dispose()
        {
              this.Context.Dispose();
              GC.SuppressFinalize(this);
        }

        private static Func<T, bool> GetDynamicExpresion(string propertyName, int val)
        {
            var param = Expression.Parameter(typeof(T), "x");
            MemberExpression member = Expression.Property(param, propertyName);
            UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);

            Expression body = Expression.Equal(member, valueExpression);
            var final = Expression.Lambda<Func<T, bool>>(body: body, parameters: param);
            return final.Compile();
        }
        private static UnaryExpression GetValueExpression(string propertyName, int val, ParameterExpression param)
        {
            var member = Expression.Property(param, propertyName);
            var propertyType = ((PropertyInfo)member.Member).PropertyType;
            var constant = Expression.Constant(val);
            return Expression.Convert(constant, propertyType);
        }

        public Task<T> Get(int id)
        {
            throw new NotImplementedException();
        }



        //public virtual IQueryable<T> Add(T entity)
        //{
        //    Context.Set<T>().Add(entity);
        //    Context.SaveChanges();
        //    var result = Context.Set<T>().Where(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id"))).AsQueryable<T>();
        //    return result;
        //}

        //public virtual int Delete(int id)
        //{
        //    var existing = Context.Set<T>().Where(GetDynamicExpresion("Id", id));
        //    if (existing != null)
        //    {
        //        Context.Set<T>().Remove(existing.First());
        //    }
        //    return Context.SaveChanges();
        //}

        //public virtual int Deactivate(int id)
        //{
        //    byte? setValue = 0;
        //    var existing = Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", id));
        //    if (existing != null)
        //    {
        //        SetPropertyValue(existing, "Activo", setValue.GetType(), setValue);
        //    }
        //    return Context.SaveChanges();
        //}

        //private bool disposed = false;

        //protected virtual void Dispose(bool disposing)
        //{
        //    if (!this.disposed && disposing)
        //    {
        //        Context.Dispose();
        //    }
        //    this.disposed = true;
        //}

        //public void Dispose()
        //{
        //    Dispose(true);
        //    GC.SuppressFinalize(this);
        //}

        //public virtual IQueryable<T> Get()
        //{
        //    return Context.Set<T>().AsQueryable<T>();
        //}

        //public virtual T Get(int id)
        //{
        //    return Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", id));
        //}

        //public virtual IQueryable<T> Update(T entity)
        //{
        //    T existing = Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id")));
        //    Context.Entry(existing).CurrentValues.SetValues(entity);
        //    Context.SaveChanges();
        //    var result = Context.Set<T>().Where(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id"))).AsQueryable<T>();
        //    return result;
        //}

        //private static Func<T, bool> GetDynamicExpresion(string propertyName, int val)
        //{
        //    var param = Expression.Parameter(typeof(T), "x");
        //    MemberExpression member = Expression.Property(param, propertyName);
        //    UnaryExpression valueExpression = GetValueExpression(propertyName, val, param);

        //    Expression body = Expression.Equal(member, valueExpression);
        //    var final = Expression.Lambda<Func<T, bool>>(body: body, parameters: param);
        //    return final.Compile();
        //}

        //private static UnaryExpression GetValueExpression(string propertyName, int val, ParameterExpression param)
        //{
        //    var member = Expression.Property(param, propertyName);
        //    var propertyType = ((PropertyInfo)member.Member).PropertyType;
        //    var constant = Expression.Constant(val);
        //    return Expression.Convert(constant, propertyType);
        //}

        //private int GetPropertyValue(T entity, string propertyName)
        //{
        //    int returnVal = 0;

        //    PropertyInfo propInfo = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //    .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

        //    if (propInfo != null)
        //    {
        //        returnVal = Convert.ToInt32(propInfo.GetValue(entity));
        //    }
        //    return returnVal;
        //}

        //private void SetPropertyValue(T entity, string propertyName, Type type, object value)
        //{

        //    PropertyInfo propInfo = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
        //    .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

        //    if (propInfo != null)
        //    {
        //        propInfo.SetValue(entity, Convert.ChangeType(value, type), null);

        //    }
        //}
    }
}
