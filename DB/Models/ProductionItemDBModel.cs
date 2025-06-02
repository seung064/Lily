using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class ProductionItemDBModel
    {
        [Key]
        public int ProductionItemNum { get; set; }
        public string ProductionItemName { get; set; }
        public DateTime ProducedAt { get; set; }
        public int ProductionItemCount { get; set; }
    }
}

