using System;
using System.Collections.Generic;
using System.Text;

namespace DataLayer.Abstraction {
    public interface IEntity <TKey>{
        TKey Id { get; set; }
    }
}
