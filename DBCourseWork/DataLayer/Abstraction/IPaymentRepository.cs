using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IPaymentRepository : IEditableRepository<Payment, int> {
        Task<IEnumerable<Payment>> GetPaymentsOfUser(int userId, int groupId);
    }
}
