using DataLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities {
    public class Payment : IEntity<int> {
        public int Id { get; set; }
        public DateTime Time { get; set; }
        public int FromUserId { get; set; }
        public int ToUserId { get; set; }
        public int GroupId { get; set; }
        public decimal Amount { get; set; }
    }
}
