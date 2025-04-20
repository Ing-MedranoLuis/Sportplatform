using Microsoft.EntityFrameworkCore;
using Proyecto.data.IRepositories;
using Proyecto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }
    
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(Expression<Func<T, bool>>? filter = null, string? IncludeProperty = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);    
            }
            if(IncludeProperty != null)
            {
                foreach(var properties in IncludeProperty.Split(new char[] {','}, StringSplitOptions.RemoveEmptyEntries))
                {
                   query= query.Include(properties);
                }
            }
            
            return query.FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperty = null, Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy = null)
        {
            IQueryable<T> query = _dbSet;
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (IncludeProperty != null)
            {
                foreach (var properties in IncludeProperty.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                 query=   query.Include(properties);
                }
            }
            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }

            return query.ToList();
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(int Id)
        {
          var obj=  _dbSet.Find(Id);
            _dbSet.Remove(obj);
        }

   
    }
}
