using System;
using System.Collections.Generic;

#nullable disable

namespace EcommerceAPI.Models
{
    public partial class OrderConfirmation
    {
        public int OrderId { get; set; }
        public int CustomerId { get; set; }
        public int ProductId { get; set; }
        public DateTime OrderDateTime { get; set; }
        public DateTime ShipDate { get; set; }
        public string ShipAddress { get; set; }
        public string BillingAddress { get; set; }
        public decimal? TotalBill { get; set; }
        public int? TransactionNumber { get; set; }
        public string PaymentMode { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual OrderDetail Order { get; set; }
    }
}
