using NetworkData.Interfaces;
using NetworkData.Repository;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkData.UnitOfWork
{
    public class UnitOfWorkData<T> : IUnitOfWorkData<T> where T : class
    {
        public readonly DataContext _dataContext;
        public IRepositoryData<T> _repository;
        public IRepositoryData<T> repository
        {
            get
            {
                return _repository ?? (_repository = new RepositoryData<T>(_dataContext));
            }
        }
        public UnitOfWorkData(DataContext dataContext)
        {
            _dataContext = dataContext;
        }
        public int Save()
        {
            return _dataContext.SaveChanges();
        }
    }
}
