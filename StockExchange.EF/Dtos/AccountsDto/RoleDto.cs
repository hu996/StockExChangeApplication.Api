﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockExChange.EF.Dtos.AccountsDto
{
    public class RoleDto
    {
        [Required]

        public string RoleName { get; set; }
    }
}
