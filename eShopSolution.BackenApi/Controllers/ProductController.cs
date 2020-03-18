using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using eShopSolution.Application.Catalog.Products;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eShopSolution.BackenApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IPublicProductService _publicProduct;
        public ProductController(IPublicProductService publicProduct)
        {
            _publicProduct = publicProduct;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _publicProduct.GetListProduct());
        }
    }
}