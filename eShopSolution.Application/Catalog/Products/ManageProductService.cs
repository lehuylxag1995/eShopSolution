using eShopSolution.Application.Catalog.Products.DTOs;
using eShopSolution.Application.Catalog.Products.DTOs.Manage;
using eShopSolution.Application.DTOs;
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly eShopDbContext _context;
        public ManageProductService(eShopDbContext context)
        {
            _context = context;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<int> Create(ProductCreateRequest request)
        {
            var product = new Product()
            {
                Price = request.Price,
                OriginalPrice = request.OriginalPrice,
                Stock = request.Stock,
                ViewCount = 0,
                DateCreated = DateTime.Now,
                ProductTranslations = new List<ProductTranslation>()
                {
                    new ProductTranslation()
                    {
                        Name=request.Name,
                        Description=request.Description,
                        Details=request.Details,
                        LanguageId=request.LanguageId,
                        SeoAlias=request.SeoAlias,
                        SeoDescription=request.SeoDescription,
                        SeoTitle=request.SeoTitle,
                    }
                }
            };
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new eShopException("Not found product");
            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }
        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetProductPagingRequest request)
        {
            //Join 4 table product
            var product = from p in _context.Products
                          join pt in _context.ProductTranslations on p.Id equals pt.ProductId
                          join pic in _context.ProductInCategories on p.Id equals pic.ProductId
                          join c in _context.Categories on pic.CategoryId equals c.Id
                          select new { p, pt, pic };
            //Filter
            if (!string.IsNullOrEmpty(request.Keyword)) // Có tìm kiếm thì
            {
                product = product.Where(x => x.pt.Name.Contains(request.Keyword));
            }

            if (request.CategoryId.Count > 0)
            {
                product = product.Where(x => request.CategoryId.Contains(x.pic.CategoryId));
            }

            //Paging
            var totalRow = await product.CountAsync();

            var data = await product.Skip((request.pageIndex - 1) * request.pageSize)
                          .Take(request.pageSize)
                          .Select(x => new ProductViewModel()
                          {
                              Id = x.p.Id,
                              DateCreated = x.p.DateCreated,
                              Description = x.pt.Description,
                              Details = x.pt.Details,
                              LanguageId = x.pt.LanguageId,
                              Name = x.pt.Name,
                              OriginalPrice = x.p.OriginalPrice,
                              Price = x.p.Price,
                              SeoAlias = x.pt.SeoAlias,
                              SeoDescription = x.pt.SeoDescription,
                              SeoTitle = x.pt.SeoTitle,
                              Stock = x.p.Stock,
                              ViewCount = x.p.ViewCount
                          }).ToListAsync();
            // Reponse
            var result = new PageResult<ProductViewModel>()
            {
                TotalPage = totalRow,
                Items = data
            };
            return result;
        }

        public async Task<int> Update(ProductUpdateRequest request)
        {
            var product = await _context.Products.FindAsync(request.Id);
            var productTranslation = await _context.ProductTranslations.FirstOrDefaultAsync(pt => pt.ProductId == request.Id && pt.LanguageId == request.LanguageId);
            if (product == null || productTranslation == null) throw new eShopException($"Cannot find id product {request.Id}");
            productTranslation.Name = request.Name;
            productTranslation.Description = request.Description;
            productTranslation.Details = request.Details;
            productTranslation.SeoAlias = request.SeoAlias;
            productTranslation.SeoDescription = request.SeoDescription;
            productTranslation.SeoTitle = request.SeoTitle;
            return await _context.SaveChangesAsync();
        }

        public async Task<bool> UpdatePrice(int productId, decimal newPrice)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new eShopException($"Cannot find id {productId}");
            product.Price = newPrice;
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateStock(int productId, int addedQuantity)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) throw new eShopException($"Cannot find id {productId}");
            product.Stock += addedQuantity;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
