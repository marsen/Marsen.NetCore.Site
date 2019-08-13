using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marsen.NetCore.Site.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult("Get");
        }

        [HttpPost]
        public JsonResult Post()
        {
            return new JsonResult("Post");
        }

        
        [HttpPut]
        public JsonResult Put()
        {
            return new JsonResult("Put");
        }
        
        [HttpDelete]
        public JsonResult Delete()
        {
            return new JsonResult("Delete");
        }
    }
}