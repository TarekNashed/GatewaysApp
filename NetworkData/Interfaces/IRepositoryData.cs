using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkData.Interfaces
{
   public interface IRepositoryData<T> where T : class
    {
        IEnumerable<T> GetAll();
        Task<IEnumerable<T>> GetAllAsync();
        T GetById(object Id);
        void Add(T entity);
        void Update(int Id,T entity);
        void Delete(T entity);
    }
}
