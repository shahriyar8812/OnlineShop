using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Code { get; set; }

        public string Description { get; set; }

        public ProductCategory Category { get; set; }
        public string ImageUrl { get; set; }
    }

    public enum ProductCategory
    {
        Clothes = 1,
        Toys = 2,
        Laptop = 3
    }
}

