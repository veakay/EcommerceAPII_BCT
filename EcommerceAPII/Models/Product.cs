using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EcommerceAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string ProductName { get; set; }
        public decimal? Price { get; set; }
        public string DescriptionP { get; set; }
        public string Image { get; set; }
        public int Stock { get; set; }
        public int CatergoryId { get; set; }

        public virtual Category Catergory { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
