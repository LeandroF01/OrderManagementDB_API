﻿using DB;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class ProductIngredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int ProductIngredientID { get; set; }

        [Required]
        public int ProductID { get; set; }

        [Required]
        public int IngredientID { get; set; }

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Quantity { get; set; }

        [ForeignKey("ProductID")]
        public virtual Products Products { get; set; }

        [ForeignKey("IngredientID")]
        public virtual Ingredients Ingredients { get; set; }

    }
}

