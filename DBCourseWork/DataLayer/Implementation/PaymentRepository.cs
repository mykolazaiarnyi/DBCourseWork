using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Implementation {
    public class PaymentRepository : IPaymentRepository {
        public Task<Payment> CreateAsync(Payment item) {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<Payment> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(Payment item) {
            throw new NotImplementedException();
        }
    }
}
