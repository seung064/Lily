using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Product
    {
        [Key]
        public int ProductNum { get; set; }

        [Required, MaxLength(100)]
        public string ProductName { get; set; }

        public string Description_P { get; set; }

        public string Receipt { get; set; }
    }
}
