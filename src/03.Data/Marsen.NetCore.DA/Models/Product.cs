using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Marsen.NetCore.DA.Models
{
    [Table("Product")]
    public class Product
    {
        [Key]
        [Column("Product_Id")]
        public long ProductId { get; set; }

        [Column("Product_Name")]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(1000)]
        [Column("Product_Information")]
        public string ProductInformation { get; set; }

        [MaxLength(1000)]
        [Column("Product_Spec")]
        public string ProductSpec { get; set; }

        [MaxLength(200)]
        [Column("Product_Picture")]
        public string ProductPicture { get; set; }
    }
}
