using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IEditableRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity: IEntity<TKey>{
        Task<bool> DeleteAsync(TKey id);
        Task<bool> UpdateAsync(TEntity item);
    }
}
