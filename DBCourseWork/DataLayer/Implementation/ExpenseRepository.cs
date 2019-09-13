using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer.Implementation {
    class ExpenseRepository : BaseRepository, IExpenseRepository {
        public async Task<Expense> GetByIdAsync(int id) {
            Expense item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from expenses_total where id = {id}", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) {
                        item = new Expense() { Id = (int)reader["id"], Description = (string)reader["description"], Time = (DateTime)reader["time"], Amount = (decimal)reader["amount"], ByUserId = (int)reader["by_user_id"], GroupId = (int)reader["group_id"] };
                    }
                }
            }
            return item;
        }

        public async Task<Expense> CreateAsync(Expense item) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"insert into expenses_total([description], group_id, [by_user_id], amount) values (N'{item.Description}', {item.GroupId}, {item.ByUserId}, {item.Amount})", connection)) {
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "select cast(SCOPE_IDENTITY() as int)";
                    int id = (int)await command.ExecuteScalarAsync();
                    item.Id = id;
                    command.CommandText = $"select [time] from expenses_total where id = {item.Id}";
                    item.Time = (DateTime)await command.ExecuteScalarAsync();
                }
            }
            return item;
        }
    }
}
