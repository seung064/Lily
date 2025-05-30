using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace Project_Lily.Models
{
    public class ProductionItem
    {
        public string Name { get; set; }

        public string ImagePath { get; set; }
        public TimeSpan Timer { get; set; }
    }
}
