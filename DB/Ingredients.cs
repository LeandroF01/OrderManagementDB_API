using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DB;

namespace DB
{
    public class Ingredients
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int IngredientID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }


        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Quantity { get; set; }

        [Required]
        [MaxLength(50)]
        public string Unit { get; set; }


        public virtual ICollection<ProductIngredients> ProductIngredients { get; set; } = new HashSet<ProductIngredients>();
    }
}


