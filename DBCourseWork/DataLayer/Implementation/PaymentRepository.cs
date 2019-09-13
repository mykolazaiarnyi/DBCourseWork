using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation {
    public class PaymentRepository : BaseRepository, IPaymentRepository {
        public async Task<Payment> CreateAsync(Payment item) {
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"insert into payments([description], group_id, [by_user_id], to_user_id, amount) values (N'{item.Description}', {item.GroupId}, {item.ByUserId}, {item.ForUserId}, {item.Amount})", connection)) {
                    await command.ExecuteNonQueryAsync();
                    command.CommandText = "select cast(SCOPE_IDENTITY() as int)";
                    int id = (int)await command.ExecuteScalarAsync();
                    item.Id = id;
                    command.CommandText = $"select [time] from payments where id = {item.Id}";
                    item.Time = (DateTime)await command.ExecuteScalarAsync();
                }
            }
            return item;
        }

        public async Task<Payment> GetByIdAsync(int id) {
            Payment item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from expenses_total where id = {id}", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) {
                        item = new Payment() { Id = (int)reader["id"], Description = (string)reader["description"], Time = (DateTime)reader["time"], Amount = (decimal)reader["amount"], ByUserId = (int)reader["by_user_id"], GroupId = (int)reader["group_id"], ForUserId =  (int)reader["for_user_id"]};
                    }
                }
            }
            return item;
        }
    }
}
