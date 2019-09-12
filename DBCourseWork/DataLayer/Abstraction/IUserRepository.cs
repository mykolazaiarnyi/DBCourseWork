using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IUserRepository : IRepository<User, int> {
        Task<IEnumerable<Group>> GetGroupsAsync(int id);
    }
}
