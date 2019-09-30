using DataLayer.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Abstraction {
    public interface IExpenseRepository : IRepository<Expense, int> {
        Task<IEnumerable<Expense>> GetExpensesOfGroupAsync(int groupId);
    }
}
