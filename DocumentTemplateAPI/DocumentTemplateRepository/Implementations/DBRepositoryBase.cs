using DocumentTemplateModel.Models;
using DocumentTemplateRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DocumentTemplateRepository.Implementations
{
    public class DBRepositoryBase<T> : IDBRepositoryBase<T>
       where T : class
    {
        private IUnitOfWork _unitOfWork;
        internal DbSet<T> dbSet;
        public DBRepositoryBase(IUnitOfWork _unitOfWork)
        {
            UnitOfWork = _unitOfWork;
            this.dbSet = UnitOfWork.CP25Team08Context.Set<T>();
        }

        /// <summary>
        /// Returns the object with the primary key specifies or throws
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T Single(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            return dbResult;
        }

        /// <summary>
        /// Returns the object with the primary key specifies or the default for the type
        /// </summary>
        /// <typeparam name="TU">The type to map the result to</typeparam>
        /// <param name="primaryKey">The primary key</param>
        /// <returns>The result mapped to the specified type</returns>
        public T SingleOrDefault(object primaryKey)
        {
            var dbResult = dbSet.Find(primaryKey);
            if (dbResult != null)
                return dbResult;
            return null;
        }

        public bool Exists(object primaryKey)
        {
            return dbSet.Find(primaryKey) != null;
        }

        public virtual object Insert(T entity)
        {
            dynamic obj = dbSet.Add(entity);
            return obj;
        }

        public virtual void BulkInsert(List<T> entity)
        {
            //dbSet.BulkInsert(entity);
        }

        public virtual void Update(T entity)
        {
            //_unitOfWork.CP25Team08Context.Entry(entity).State = EntityState.Modified;
        }

        public virtual void BulkUpdate(List<T> entity)
        {
            //dbSet.BulkUpdate(entity);
        }

        public void Delete(T entity)
        {
            //if (_unitOfWork.CP25Team08Context.Entry(entity).State == EntityState.Detached)
            //{
            //    dbSet.Attach(entity);
            //}
            dbSet.Remove(entity);
        }

        public IUnitOfWork UnitOfWork { get { return _unitOfWork; } set { _unitOfWork = value; } }
        internal CP25Team08Context Database { get { return _unitOfWork.CP25Team08Context; } }

        public IQueryable<T> GetAll()
        {
            return dbSet.AsQueryable();
        }

        public void Dispose() => _unitOfWork.Dispose();
        public void Commit() => _unitOfWork.Commit();



    }
}
