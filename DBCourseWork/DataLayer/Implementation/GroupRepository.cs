using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation {
    public class GroupRepository : IGroupRepository {
        public Task AddUserAsync(int groupId, int userId) {
            throw new NotImplementedException();
        }

        public Task CreateAsync(Group item) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<Group> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<User>> GetUsersAsync(int id) {
            throw new NotImplementedException();
        }

        public Task RemoveUserAsync(int groupId, int userId) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Group item) {
            throw new NotImplementedException();
        }
    }
}
