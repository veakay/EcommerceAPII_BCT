using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using EcommerceAPI.Models;

namespace EcommerceAPI.DTO
{
    public class ProductDto
    {
    
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string DescriptionP { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public string CategoryName { get; set; }
        public int CatergoryId { get; set; }
    }
}
