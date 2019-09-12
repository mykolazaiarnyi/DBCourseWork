using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IGroupRepository : IEditableRepository<Group, int> {
        Task AddUserAsync(int groupId, int userId);
        Task<bool> RemoveUserAsync(int groupId, int userId);
        Task<IEnumerable<User>> GetUsersAsync(int id);
    }
}
