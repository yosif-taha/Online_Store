using Online_Store.Domain.Contracts;
using Online_Store.Domain.Entites;
using Online_Store.Persistence.Data.Contexts;
using Online_Store.Persistence.Repositories;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Online_Store.Persistence
{
    public class UnitOfWork(OnlineStoreDbContext _context) : IUnitOfWork
    {
       /* private Dictionary<string, object> _Repository = new Dictionary<string, object>();*/  //This Dectionart is container have all repository are used.

        private ConcurrentDictionary<string, object> _Repository = new ConcurrentDictionary<string, object>();
        //public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        //{
        //    var type = typeof(TEntity).Name;  // typeof .Name to return name of any class, to use this in if condition.
        //    if(!_Repository.ContainsKey(type))  //Check in dictionary (_Repository) have this repository of type Tentity or Not. 
        //    {
        //        var repository = new GenericRepository<TKey,TEntity>(_context);  //Create new object from generic repository
        //        _Repository.Add(type,repository);  // added this new repo in dectionary
        //    }
        //    return (IGenericRepository<TKey, TEntity>) _Repository[type];
        //}
        public IGenericRepository<TKey, TEntity> GetRepository<TKey, TEntity>() where TEntity : BaseEntity<TKey>
        {

            // this function inside 'ConcurrentDictionary' used to check repository exists in this dictionary or not , if nor create a new repo , if exists return her. 
            return (IGenericRepository<TKey, TEntity>) _Repository.GetOrAdd(typeof(TEntity).Name , new GenericRepository<TKey,TEntity>(_context) );
        }

        public async Task<int> SaveChangeAsync()
        {
           return await _context.SaveChangesAsync();
        }
    }
}
