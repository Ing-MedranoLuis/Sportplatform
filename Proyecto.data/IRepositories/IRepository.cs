﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.IRepositories
{
    public interface IRepository<T> where T : class
    {
        T Get(Expression<Func<T, bool>>? filter = null, string? IncludeProperty = null);

        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? IncludeProperty = null,Func<IQueryable<T>,IOrderedQueryable<T>>? orderBy=null);
        void Add(T entity);


        void Remove(T entity);
        void Remove(int Id);    
    }
}
