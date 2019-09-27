using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation {
    public class GroupRepository : BaseRepository, IGroupRepository {
        public async Task AddUserAsync(int groupId, int userId) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"insert into user_groups ([user_id], group_id) values ({userId}, {groupId})", connection)) {
                    await command.ExecuteNonQueryAsync();
                }
            }
        }

        public async Task<Group> CreateAsync(Group item) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"insert into groups ([name]) values (N'{item.Name}')", connection)) {
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
                using (SqlCommand command = new SqlCommand($"delete from groups where id = {id}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }

        public async Task<Group> GetByIdAsync(int id) {
            Group item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from groups where id = {id}", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) {
                        item = new Group() { Id = (int)reader["id"], Name = (string)reader["name"] };
                    }
                }
            }
            return item;
        }

        public async Task<IEnumerable<User>> GetUsersAsync(int groupId, int userId) {
            List<User> users = new List<User>();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from get_users_of_group({groupId}, {userId})", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        users.Add(new User() { Id = (int)reader["id"], Name = (string)reader["name"] });
                    }
                }
            }
            return users;
        }

        public async Task<bool> RemoveUserAsync(int groupId, int userId) {
            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"delete from user_groups where [user_id] = {userId} and group_id = {groupId}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }

        public async Task<bool> UpdateAsync(Group item) {
            int result;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"update groups set name = N'{item.Name}' where id = {item.Id}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }
    }
}
