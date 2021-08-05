using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace NetworkData.Interfaces
{
    public interface IUnitOfWorkData<T> where T : class
    {
        IRepositoryData<T> repository { get; }
        int Save();
    }
}
