using DataLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities {
    public class Payment : Expense {
        public int ForUserId { get; set; }
        public bool Confirmed { get; set; }
    }
}
