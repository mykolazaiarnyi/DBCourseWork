using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Abstraction {
    public interface IPaymentRepository : IRepository<Payment, int> {
    }
}
