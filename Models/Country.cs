﻿using System.ComponentModel.DataAnnotations;
using CoreApiBooks.Repositories;

namespace CoreApiBooks.Models
{
    public class Country : BaseEntity
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }
    }
}