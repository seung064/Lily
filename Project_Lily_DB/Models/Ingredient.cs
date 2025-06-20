﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project_Lily_DB.Models
{
    public class Ingredient
    {
        [Key]
        public int IngredientNum { get; set; }

        [Required, MaxLength(100)]
        public string IngredientName { get; set; }

        public string Description_I { get; set; }

        public string IngredientImage { get; set; }


        [ForeignKey("Country")]
        public int CountryNum { get; set; }
        public Country Country { get; set; }
    }
}
