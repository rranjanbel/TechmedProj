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
            Context = context;
        }

        public virtual IQueryable<T> Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            var result = Context.Set<T>().Where(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id"))).AsQueryable<T>();
            return result;
        }

        public virtual int Delete(int id)
        {
            var existing = Context.Set<T>().Where(GetDynamicExpresion("Id", id));
            if (existing != null)
            {
                Context.Set<T>().Remove(existing.First());
            }
            return Context.SaveChanges();
        }

        public virtual int Deactivate(int id)
        {
            byte? setValue = 0;
            var existing = Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", id));
            if (existing != null)
            {
                SetPropertyValue(existing, "Activo", setValue.GetType(), setValue);
            }
            return Context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed && disposing)
            {
                Context.Dispose();
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual IQueryable<T> Get()
        {
            return Context.Set<T>().AsQueryable<T>();
        }

        public virtual T Get(int id)
        {
            return Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", id));
        }

        public virtual IQueryable<T> Update(T entity)
        {
            T existing = Context.Set<T>().FirstOrDefault(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id")));
            Context.Entry(existing).CurrentValues.SetValues(entity);
            Context.SaveChanges();
            var result = Context.Set<T>().Where(GetDynamicExpresion("Id", GetPropertyValue(entity, "Id"))).AsQueryable<T>();
            return result;
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

        private int GetPropertyValue(T entity, string propertyName)
        {
            int returnVal = 0;

            PropertyInfo propInfo = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (propInfo != null)
            {
                returnVal = Convert.ToInt32(propInfo.GetValue(entity));
            }
            return returnVal;
        }

        private void SetPropertyValue(T entity, string propertyName, Type type, object value)
        {

            PropertyInfo propInfo = entity.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .FirstOrDefault(x => x.Name.Equals(propertyName, StringComparison.OrdinalIgnoreCase));

            if (propInfo != null)
            {
                propInfo.SetValue(entity, Convert.ChangeType(value, type), null);

            }
        }
    }
}
