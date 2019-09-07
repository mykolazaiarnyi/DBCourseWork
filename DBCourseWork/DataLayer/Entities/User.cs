using DataLayer.Abstraction;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Entities {
    public class User : IEntity<int> {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Group> Groups { get; set; }
    }
}
