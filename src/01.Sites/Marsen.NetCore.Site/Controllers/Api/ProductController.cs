using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Marsen.Business.Logic.Interface;
using Marsen.NetCore.DA.Storage;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Marsen.NetCore.Site.Controllers.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductStorage _productStorage;

        /// <summary>
        /// Initializes a new instance of the <see cref="ProductController" /> class.
        /// </summary>
        public ProductController(IProductStorage productStorage)
        {
            this._productStorage = productStorage;
        }
        [HttpGet("{id}")]
        public JsonResult Get(long id)
        {
            return new JsonResult(this._productStorage.Read(id));
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