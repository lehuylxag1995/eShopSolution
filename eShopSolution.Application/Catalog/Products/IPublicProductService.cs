using eShopSolution.Application.DTOs;
using eShopSolution.Application.Catalog.Products.DTOs;
using eShopSolution.Application.Catalog.Products.DTOs.Public;

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IPublicProductService
    {
        Task<PageResult<ProductViewModel>> GetAllProductByCategoryId(GetProductPagingRequest request);
    }
}
