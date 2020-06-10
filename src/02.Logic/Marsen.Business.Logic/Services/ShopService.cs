using Marsen.Business.Logic.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Marsen.Business.Logic.Services
{
    public class ShopService
    {
        public ShopEntity Get(long id)
        {
            //// TODO: Get Data From DAO
            var shopList = new List<ShopEntity>
            {
                new ShopEntity {Id = 1, IsEnable = true, Title = "Marsen Shop"},
                new ShopEntity {Id = 2, IsEnable = false, Title = "Marsen Shop Close"},
                new ShopEntity {Id = 3, IsEnable = true, Title = "Kobe Shop"},
                new ShopEntity {Id = 4, IsEnable = true, Title = "Tom's Shop"},
            };
            return shopList.First(s => s.Id == id);
        }
    }
}