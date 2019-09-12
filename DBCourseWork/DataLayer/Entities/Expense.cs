using DataLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities {
    public class Expense : IEntity<int> {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public int GroupId { get; set; }
        public int ByUserId { get; set; }
        public decimal Amount { get; set; }
    }
}
