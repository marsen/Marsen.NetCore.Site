using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Marsen.Business.Logic.Entities;
using Marsen.NetCore.DA.Models;

namespace Marsen.NetCore.DA.Mapper
{
    public class ShopMapping:Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ShopMapping" /> class.
        /// </summary>
        public ShopMapping()
        {
            this.CreateMap<ShopEntity, Shop>()
                .ForMember(i => i.ShopId, s => s.MapFrom(i => i.Id))
                .ForMember(i => i.ShopIsEnable, s => s.MapFrom(i => i.IsEnable))
                .ForMember(i => i.ShopTitle, s => s.MapFrom(i => i.Title));

            this.CreateMap<ProductEntity, Product>()
                .ForMember(i => i.ProductId, j => j.MapFrom(i => i.Id));
            this.CreateMap<Product, ProductEntity>()
                .ForMember(i => i.Id, j => j.MapFrom(i => i.ProductId));
        }
    }
}
