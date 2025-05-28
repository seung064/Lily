using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProjectDB.Models
{
    public class Country
    {
        [Key]
        public int CountryNum { get; set; }

        [Required] // 필수 입력 (NOT NULL)
        [MaxLength(100)] // 최대 100자 제한
        public string CountryName { get; set; }

        public string CountryProperty { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
