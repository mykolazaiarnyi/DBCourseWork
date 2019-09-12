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
    public class UserRepository : BaseRepository, IUserRepository {
        public UserRepository() : base() { }

        public async Task<User> CreateAsync(User item) {
            using(SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand($"insert into users ([name]) values (N'{item.Name}')", connection)) {
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "select cast(SCOPE_IDENTITY() as int)";
                    int id = (int)await command.ExecuteScalarAsync();
                    item.Id = id;
                }
            }
            return item;
        }

        public async Task<bool> DeleteAsync(int id) {
            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"delete from users where id = {id}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }

        public async Task<User> GetByIdAsync(int id) {
            User item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from users where id = {id}", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) {
                        item = new User() { Id = (int)reader["id"], Name = (string)reader["name"] };
                    }
                }
            }
            return item;
        }

        public async Task<IEnumerable<Group>> GetGroupsAsync(int id) {
            List<Group> groups = new List<Group>();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from get_groups_of_user({id})", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        groups.Add(new Group() { Id = (int)reader["id"], Name = (string)reader["name"] });
                    }
                }
            }
            return groups;
        }

        public async Task<bool> UpdateAsync(User item) {
            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"update users set name = N'{item.Name}' where id = {item.Id}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }
    }
}
