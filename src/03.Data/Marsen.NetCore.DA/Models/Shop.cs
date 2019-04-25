using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Marsen.NetCore.DA.Models
{
    public class Shop
    {
        [Key]
        public long Shop_Id { get; set; }
        public string Shop_Title { get; set; }
        public bool Shop_IsEnable { get; set; }

    }
}