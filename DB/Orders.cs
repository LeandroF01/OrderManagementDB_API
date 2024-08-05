using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Orders
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int OrderID { get; set; }

        [Required]
        public int UserID { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        [MaxLength(50)]
        public string Status { get; set; } // e.g. Pending, In Process, Completed, Canceled

        [Required]
        [MaxLength(50)]
        public string OrderType { get; set; } // e.g. Dine-In, Delivery

        [Required]
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Total { get; set; }

        // Navigation properties
        [ForeignKey("UserID")]
        public virtual Users Users { get; set; }

        public virtual ICollection<OrderDetails> OrderDetails { get; set; } = new HashSet<OrderDetails>();
        public virtual ICollection<PaymentMethods> PaymentMethods { get; set; } = new HashSet<PaymentMethods>();
    }
}
