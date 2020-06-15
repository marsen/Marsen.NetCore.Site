using Marsen.Business.Logic.Entities;

namespace Marsen.Business.Logic.Interfaces
{
    public interface IShopService
    {
        ShopEntity Get(long id);
    }
}