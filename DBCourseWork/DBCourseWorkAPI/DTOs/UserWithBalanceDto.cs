﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.DTOs {
    public class UserWithBalanceDto : UserDto {
        public decimal Balance { get; set; }
    }
}
