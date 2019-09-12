using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer.Implementation {
    class ExpenseRepository : IExpenseRepository {
        public Task CreateAsync(Expense item) {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        public Task<Expense> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(Expense item) {
            throw new NotImplementedException();
        }
    }
}
