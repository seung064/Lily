using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectDB.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientNum { get; set; }

        [Required, MaxLength(100)]
        public string IngredientName { get; set; }

        public string Description_I { get; set; }


        [ForeignKey("Country")]
        public int CountryNum { get; set; }
        public Country Country { get; set; }
    }
}
