using Marsen.Business.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marsen.NetCore.Site.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            var result = _shopService.Get(id);
            return Ok(result);
        }
    }
}