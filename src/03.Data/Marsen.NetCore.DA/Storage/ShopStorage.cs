using System;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Services;

namespace Marsen.NetCore.DA.Storage
{
    public class ShopStorage:IRead<ShopEntity>
    {
        public ShopEntity Read(long id)
        {
            using (var context=new PhobosContext())
            {
                var result = context.Shop.FirstOrDefault(x => x.ShopId == id);
                return AutoMapper.Mapper.Map<ShopEntity>(result);
            }
        }
    }
}
