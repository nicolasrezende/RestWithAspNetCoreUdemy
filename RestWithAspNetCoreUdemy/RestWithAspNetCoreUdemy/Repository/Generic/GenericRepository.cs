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
        protected readonly MysqlContext _context;
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

        public List<T> FindAll() => _dataSet.ToList();

        public T FindById(long id) => _dataSet.Find(id);

        public List<T> FindWithPagedSearch(string query) => _dataSet.FromSql<T>(query).ToList();

        public int GetCount(string query)
        {
            var result = "";
            using (var connection = _context.Database.GetDbConnection())
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    result = command.ExecuteScalar().ToString();
                }
            }
            return int.Parse(result);
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
