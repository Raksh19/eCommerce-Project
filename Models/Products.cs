using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace eCommerce.Models
{
    public class Products
    {
        public int Id { get; set; }
        [Required]

        public string Name { get; set; }
        [Required]

        public string Description { get; set; }

        public string Supplier { get; set; }

        public decimal Price { get; set; }

        public string Image { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        

        
    }
}
