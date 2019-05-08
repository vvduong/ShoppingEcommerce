using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingEcommerce.Services.DTOs
{
    public class ProductDTO
    {
        public int ProductID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string MetaTitle { get; set; }
        public string Description { get; set; }
        public string ProductImage { get; set; }
        public string MoreImages { get; set; }
        public double Price { get; set; }
    }
}
