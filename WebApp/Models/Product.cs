using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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

        [Required]
        [Display(Name = "Product Name")]
        public string ProductName { get; set; }

        [Display(Name = "Information")]
        public string ProductInfo { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductRelease { get; set; }

        [Display(Name = "Modified Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyyy}", ApplyFormatInEditMode = true)]
        public DateTime ProductModified { get; set; }

        [Display(Name = "Status")]
        public ProductStatus ProductStatus { get; set; }
        public decimal Price { get; set; }

        public int? UserID { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<ProductMeta> ProductMetas { get; set; }
    }
}