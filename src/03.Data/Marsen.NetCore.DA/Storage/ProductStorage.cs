using System;
using System.Linq;
using AutoMapper;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Services;
using Marsen.NetCore.DA.Models;

namespace Marsen.NetCore.DA.Storage
{
    public class ProductStorage:IRead<ProductEntity>
    {
        private readonly IMapper _mapper;

        public ProductStorage(IMapper mapper)
        {
            _mapper = mapper;
        }
        public ProductEntity Read(long id)
        {
            using (var context=new MARSContext())
            {
                var result = context.Product.FirstOrDefault(x => x.ProductId == id);
                return _mapper.Map<ProductEntity>(result);
            }
        }
    }
}
