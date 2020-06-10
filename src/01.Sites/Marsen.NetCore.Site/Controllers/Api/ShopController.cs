using Marsen.Business.Logic.Services;
using Microsoft.AspNetCore.Mvc;

namespace Marsen.NetCore.Site.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            ShopService shopService = new ShopService();
            var result = shopService.Get(id);
            return Ok(result);
        }
    }
}