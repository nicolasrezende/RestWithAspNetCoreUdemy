using Microsoft.EntityFrameworkCore;
using RestWithAspNetCoreUdemy.Models.Base;
using RestWithAspNetCoreUdemy.Models.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RestWithAspNetCoreUdemy.Repository.Generic
{
    public class GenericRepository<T> : IRepository<T> where T : BaseEntity
    {
        private readonly MysqlContext _context;
        private DbSet<T> _dataSet;

        public GenericRepository(MysqlContext context)
        {
            _context = context;
            _dataSet = context.Set<T>();
        }

        public T Create(T entity)
        {
            try
            {
                _dataSet.Add(entity);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return entity;
        }

        public void Delete(long id)
        {
            var result = FindById(id);
            try
            {
                if (result != null)
                {
                    _context.Remove(result);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<T> FindAll()
        {
            return _dataSet.ToList();
        }

        public T FindById(long id)
        {
            return _dataSet.Find(id);
        }

        public T Update(T entity)
        {
            var result = FindById(entity.Id);
            try
            {
                if (result != null)
                {
                    _context.Entry(result).CurrentValues.SetValues(entity);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }
    }
}
