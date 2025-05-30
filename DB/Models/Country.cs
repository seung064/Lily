using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB.Models
{
    public class Country
    {
        [Key]
        public int CountryNum { get; set; }

        public string CountryName { get; set; }

        public string CountryProperty { get; set; }

    }
}
