using System;
using System.Linq;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Services;
using Marsen.NetCore.DA.Models;

namespace Marsen.NetCore.DA.Storage
{
    public class ProductStorage:IRead<ProductEntity>
    {
        public ProductEntity Read(long id)
        {
            using (var context=new MARSContext())
            {
                var result = context.Product.FirstOrDefault(x => x.ProductId == id);
                return AutoMapper.Mapper.Map<ProductEntity>(result);
            }
        }
    }
}
