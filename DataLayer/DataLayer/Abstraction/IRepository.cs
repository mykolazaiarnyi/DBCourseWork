using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IRepository <TEntity, TKey> where TEntity: IEntity<TKey> {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByIdAsync(TKey id);
        Task CreateAsync(TEntity item);
        Task DeleteAsync(TKey id);
        Task UpdateAsync(TEntity item);
    }
}
