using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB
{
    public class Tables
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TableID { get; set; }

        [Required]
        public int TableNumber { get; set; }

        [Required]
        public float PositionX { get; set; }

        [Required]
        public float PositionY { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = "Available"; // Default value

        [Required]
        public int Capacity { get; set; }

        // Navigation property to Orders
        public virtual ICollection<Orders> Orders { get; set; } = new HashSet<Orders>();

    }
}
