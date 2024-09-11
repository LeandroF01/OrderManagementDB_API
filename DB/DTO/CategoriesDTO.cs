using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.DTO
{
    internal class CategoriesDTO
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // Auto-incremental
        public int CategoryID { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

    }
}
