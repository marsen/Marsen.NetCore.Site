using System;
using Marsen.Business.Logic.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Marsen.NetCore.Site.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShopController : ControllerBase
    {
        private readonly IShopService _shopService;
        private readonly ILogger<ShopController> _logger;

        public ShopController(IServiceProvider serviceProvider)
        {
            _shopService = serviceProvider.GetRequiredService<IShopService>();
            _logger = serviceProvider.GetRequiredService<ILogger<ShopController>>();
        }

        [HttpGet("{id}")]
        public ActionResult<string> Get(long id)
        {
            _logger.LogTrace(id.ToString());
            var result = _shopService.Get(id);
            return Ok(result);
        }
    }
}