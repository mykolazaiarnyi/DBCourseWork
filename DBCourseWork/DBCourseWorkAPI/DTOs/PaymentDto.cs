using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DBCourseWorkAPI.DTOs {
    public class PaymentDto {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Time { get; set; }
        public string ByUser { get; set; }
        public decimal Amount { get; set; }
        public string ForUser { get; set; }
        public bool Confirmed { get; set; }
    }
}
