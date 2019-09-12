using DataLayer.Abstraction;
using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DataLayer.Implementation {
    class ExpenseRepository : IExpenseRepository {
        public Task<Expense> GetByIdAsync(int id) {
            throw new NotImplementedException();
        }

        Task<Expense> IRepository<Expense, int>.CreateAsync(Expense item) {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<Expense, int>.DeleteAsync(int id) {
            throw new NotImplementedException();
        }

        Task<bool> IRepository<Expense, int>.UpdateAsync(Expense item) {
            throw new NotImplementedException();
        }
    }
}
