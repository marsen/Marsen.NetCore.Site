using System;
using System.Linq;
using Marsen.Business.Logic.Entities;
using Marsen.Business.Logic.Services;
using Marsen.NetCore.DA.Models;

namespace Marsen.NetCore.DA.Storage
{
    public interface IProductStorage:IRead<ProductEntity>
    {        
    }
}
