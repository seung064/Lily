using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Cryptography.X509Certificates;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace Project_Lily_DB.Models
{
    public class Country
    {
        [Key]
        public int CountryNum { get; set; }

        public string CountryName { get; set; }

        public string CountryProperty { get; set; }

        public string CountryImage { get; set; }
    }
}
