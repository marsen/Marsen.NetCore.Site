using System.ComponentModel.DataAnnotations;

namespace Marsen.NetCore.DA.Models
{
    public class Shop
    {
        [Key]
        public long ShopId { get; set; }
        public string ShopTitle { get; set; }
        public bool ShopIsEnable { get; set; }

    }
}