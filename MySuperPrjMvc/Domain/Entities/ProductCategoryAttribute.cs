using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ProductCategoryAttribute
    {
        public int Id { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int CategoryAttributeId { get; set; }
        public CategoryAttribute CategoryAttribute { get; set; }

        public string Value { get; set; }
    }

}
