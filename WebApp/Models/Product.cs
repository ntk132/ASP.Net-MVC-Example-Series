using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApp.Models
{
    public enum ProductStatus
    {
        Available,
        SoldOut,
    }
    public class Product
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string ProductInfo { get; set; }
        public DateTime ProductRelease { get; set; }
        public DateTime ProductModified { get; set; }
        public ProductStatus ProductStatus { get; set; }
        public decimal Price { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }
    }
}