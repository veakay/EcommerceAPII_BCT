using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace EcommerceAPI.Models
{
    public partial class Cart
    {
        public Cart()
        {
            OrderDetails = new HashSet<OrderDetail>();
        }
        [Key]
        public int Id { get; set; }
        public int CartNo { get; set; }
      
        public string ProductName { get; set; }
        public string ProductId { get; set; }
        public string Sku { get; set; }
        public decimal? Price { get; set; }
        public string Quantity { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderDetail> OrderDetails { get; set; }
    }
}
