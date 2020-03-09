using eShopSolution.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using eShopSolution.ViewModels.Common;
using eShopSolution.ViewModels.Catalog.Products;

namespace eShopSolution.Application.Catalog.Products
{
    public class PublicProductService : IPublicProductService
    {
        private readonly eShopDbContext _context;
        public PublicProductService(eShopDbContext context)
        {
            _context = context;
        }
        public async Task<PageResult<ProductViewModel>> GetAllProductByCategoryId(GetPublicProductPagingRequest request)
        {
            var listProducts = from p in _context.Products
                               join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                               join pic in _context.ProductInCategories on pt.ProductId equals pic.ProductId
                               join c in _context.Categories on pic.CategoryId equals c.Id
                               select new { p, pt, pic };
            if (request.CategoryId.HasValue && request.CategoryId.Value > 0)
                listProducts = listProducts.Where(x => x.pic.CategoryId == request.CategoryId);
            //Paging
            int totalRow = await listProducts.CountAsync();
            var data = await listProducts.Skip((request.pageIndex - 1) * request.pageSize)
                                .Take(request.pageSize)
                                .Select(x => new ProductViewModel()
                                {
                                    Id = x.p.Id,
                                    ViewCount = x.p.ViewCount,
                                    DateCreated = x.p.DateCreated,
                                    Description = x.pt.Description,
                                    Details = x.pt.Details,
                                    Name = x.pt.Name,
                                    Price = x.p.Price,
                                    OriginalPrice = x.p.OriginalPrice,
                                    Stock = x.p.Stock,
                                    SeoAlias = x.pt.SeoAlias,
                                    SeoTitle = x.pt.SeoTitle,
                                    SeoDescription = x.pt.SeoDescription,
                                    LanguageId = x.pt.LanguageId
                                }).ToListAsync();

            return new PageResult<ProductViewModel>()
            {
                Items = data,
                TotalPage = totalRow
            };
        }

        public async Task<List<ProductViewModel>> GetListProduct()
        {
            var listProducts = from p in _context.Products
                               join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                               join pic in _context.ProductInCategories on pt.ProductId equals pic.ProductId
                               join c in _context.Categories on pic.CategoryId equals c.Id
                               select new { p, pt, pic };
            var data = await listProducts.Select(x => new ProductViewModel()
            {
                Id = x.p.Id,
                ViewCount = x.p.ViewCount,
                DateCreated = x.p.DateCreated,
                Description = x.pt.Description,
                Details = x.pt.Details,
                Name = x.pt.Name,
                Price = x.p.Price,
                OriginalPrice = x.p.OriginalPrice,
                Stock = x.p.Stock,
                SeoAlias = x.pt.SeoAlias,
                SeoTitle = x.pt.SeoTitle,
                SeoDescription = x.pt.SeoDescription,
                LanguageId = x.pt.LanguageId
            }).ToListAsync();
            return data;
        }
    }
}
