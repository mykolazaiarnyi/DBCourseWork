using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlTypes;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace DataLayer.Implementation {
    public class UserRepository : IUserRepository {
        public Task CreateAsync(User item) {
            using(SqlConnection connection = new SqlConnection()) {

            }
        }

        public Task DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<User> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Group>> GetGroupsAsync(int id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(User item) {
            throw new NotImplementedException();
        }
    }
}
