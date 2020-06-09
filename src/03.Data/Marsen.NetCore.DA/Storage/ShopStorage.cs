using AutoMapper;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Services;
using Marsen.NetCore.DA.Models;
using System.Linq;

namespace Marsen.NetCore.DA.Storage
{
    public class ShopStorage : IRead<ShopEntity>
    {
        private readonly IMapper _mapper;

        public ShopStorage(IMapper mapper)
        {
            _mapper = mapper;
        }

        public ShopEntity Read(long id)
        {
            using (var context = new PhobosContext())
            {
                var result = context.Shop.FirstOrDefault(x => x.ShopId == id);
                return _mapper.Map<ShopEntity>(result);
            }
        }
    }
}