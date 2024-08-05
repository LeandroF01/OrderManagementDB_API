using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class PaymentMethods
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int PaymentID { get; set; }

        [Required]
        public int OrderID { get; set; }

        [Required]
        [MaxLength(50)]
        public string PaymentType { get; set; } // e.g. Cash, Card, PayPal

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Amount { get; set; }

        // Navigation property
        [ForeignKey("OrderID")]
        public virtual Orders Orders { get; set; }
    }
}
