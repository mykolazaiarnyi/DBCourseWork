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
                using (SqlCommand command = new SqlCommand($"insert into payments([description], group_id, [user_id], to_user_id, amount) values (N'{item.Description}', {item.GroupId}, {item.ByUserId}, {item.ForUserId}, {item.Amount})", connection)) {
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

        public Task<bool> DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        public async Task<Payment> GetByIdAsync(int id) {
            Payment item = null;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"select * from payments where id = {id}", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    if (await reader.ReadAsync()) {
                        item = new Payment() { 
                            Id = (int)reader["id"], 
                            Description = ((string)reader["description"]).TrimEnd(), 
                            Time = (DateTime)reader["time"], 
                            Amount = (decimal)reader["amount"], 
                            ByUserId = (int)reader["user_id"], 
                            GroupId = (int)reader["group_id"], 
                            ForUserId = (int)reader["to_user_id"],
                            Confirmed = (int)reader["confirmed"] == 1
                        };
                    }
                }
            }
            return item;
        }

        public async Task<IEnumerable<Payment>> GetPaymentsOfUser(int userId, int groupId) {
            var payments = new List<Payment>();
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using(SqlCommand command = new SqlCommand($"select * from payments where group_id = {groupId} and ([user_id] = {userId} or to_user_id = {userId}) order by [time]", connection)) {
                    SqlDataReader reader = await command.ExecuteReaderAsync();
                    while (await reader.ReadAsync()) {
                        var payment = new Payment {
                            Id = (int)reader["id"],
                            Description = ((string)reader["description"]).TrimEnd(),
                            Time = (DateTime)reader["time"],
                            Amount = (decimal)reader["amount"],
                            ByUserId = (int)reader["user_id"],
                            GroupId = (int)reader["group_id"],
                            ForUserId = (int)reader["to_user_id"],
                            Confirmed = (bool)reader["confirmed"],
                        };
                        payments.Add(payment);
                    }
                }
            }
            return payments;
        }

        public async Task<bool> UpdateAsync(Payment item) {
            int result = 0;
            using (SqlConnection connection = new SqlConnection(_connectionString)) {
                connection.Open();
                using (SqlCommand command = new SqlCommand($"update payments set confirmed = N'{(item.Confirmed? 1 : 0)}' where id = {item.Id}", connection)) {
                    result = await command.ExecuteNonQueryAsync();
                }
            }
            return result == 1;
        }
    }
}
