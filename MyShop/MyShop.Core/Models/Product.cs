﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Core.Models
{
    public class Product : BaseEntity
    {
        [StringLength(20)] //Maximum string length = 20
        [DisplayName("Product Name")] //Useful for labels in the scaffolding
        public string Name { get; set; }
        public string Description { get; set; }

        [Range(0,1000)] //Price should be in this range
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string Image { get; set; } //URL for the project image
    }
}
