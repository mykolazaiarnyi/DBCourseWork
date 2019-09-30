using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBCourseWorkAPI.DTOs {
    public class ExpenseDto {
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string ByUserName { get; set; }
        public decimal Amount { get; set; }
    }
}
