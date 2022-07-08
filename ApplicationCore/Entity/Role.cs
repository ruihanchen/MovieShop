﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationCore.Entity
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(20)]
        public string? Name { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }
    }
}
