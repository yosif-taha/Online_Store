using Online_Store.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Domain.Contracts
{
    public interface IUnitOfWork
    {
        IGenericRepository<TKey, TEntity> GetRepository<TKey,TEntity>() where TEntity : BaseEntity<TKey> ;

        Task<int> SaveChangeAsync();
    }
}
