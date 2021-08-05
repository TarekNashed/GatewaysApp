using Microsoft.EntityFrameworkCore;
using NetworkData.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace NetworkData.Repository
{
    public class RepositoryData<T> : IRepositoryData<T> where T : class
    {
        private readonly DataContext _context;
        private Microsoft.EntityFrameworkCore.DbSet<T> table = null;
        public RepositoryData(DataContext context)
        {
            _context = context;
            table = _context.Set<T>();
        }
        public void Add(T entity)
        {
            table.Add(entity);
        }

        public void Delete(T entity)
        {
            table.Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return table.ToList();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await table.ToListAsync();
        }

        public T GetById(object Id)
        {
            return table.Find(Id);
        }
        public void Update(int Id,T entity)
        {
            var originalEntity = GetById(Id);
            Update(originalEntity, entity);
        }
        public void Update(T originalEntity, object newEntity)
        {
            _context.Entry(originalEntity).CurrentValues.SetValues(newEntity);
        }
    }
}
