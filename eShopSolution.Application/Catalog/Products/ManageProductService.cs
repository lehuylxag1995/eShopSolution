
using eShopSolution.Data.EF;
using eShopSolution.Data.Entities;
using eShopSolution.Utilities.Exceptions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Net.Http.Headers;
using System.IO;
using eShopSolution.Application.Common;

namespace eShopSolution.Application.Catalog.Products
{
    public class ManageProductService : IManageProductService
    {
        private readonly eShopDbContext _context;
        private readonly IStorageService _storageService;
        public ManageProductService(eShopDbContext context, IStorageService storageService)
        {
            _context = context;
            _storageService = storageService;
        }

        private async Task<string> SaveFile(IFormFile file)
        {
            var originalFileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(originalFileName)}";
            await _storageService.SaveFileAsync(file.OpenReadStream(), fileName);
            return fileName;
        }

        public async Task AddViewCount(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            product.ViewCount += 1;
            await _context.SaveChangesAsync();
        }

        public async Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request)
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
            //Save image
            if (request.ThumbnailImage != null)
            {
                product.ProductImages = new List<ProductImage>()
                {
                    new ProductImage()
                    {
                        Caption="Thumbnail Images",
                        DateCreated=DateTime.Now,
                        FileSize=request.ThumbnailImage.Length,
                        ImagePath=await this.SaveFile(request.ThumbnailImage),
                        IsDefault=true,
                        SortOrder=1
                    }
                };
            }
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync();
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
            //Update Images
            if (request.ThumbnailImage != null)
            {
                var infoThumbnail = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == request.Id && x.IsDefault == true);
                if (infoThumbnail != null)
                {
                    infoThumbnail.ImagePath = await SaveFile(request.ThumbnailImage);
                    infoThumbnail.FileSize = request.ThumbnailImage.Length;
                    _context.ProductImages.Update(infoThumbnail);
                }

            }
            return await _context.SaveChangesAsync();
        }

        public async Task<int> Delete(int ProductId)
        {
            var product = await _context.Products.FindAsync(ProductId);
            if (product == null) throw new eShopException("Not found product");

            var images = _context.ProductImages.Where(pi => pi.ProductId == product.Id);
            foreach (var image in images)
            {
                // Xoá ảnh vật lý
                await _storageService.DeleteFileAsync(image.ImagePath);
            }

            _context.Products.Remove(product);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> AddImages(int productId, List<IFormFile> listFile)
        {
            int sortOrderNew = 0;
            var productById = await _context.ProductImages.FirstOrDefaultAsync(x => x.ProductId == productId);
            if (productById == null)
                throw new eShopException($"Cannot find productId:{productId}");
            //Saving to database
            var productImage = _context.ProductImages.Where(x => x.ProductId == productId);
            if (productImage != null)
            {
                var lastImage = await productImage.LastOrDefaultAsync();
                sortOrderNew = lastImage.SortOrder;
            }
            var listProductImages = new List<ProductImage>();
            foreach (var file in listFile)
            {
                listProductImages.Add(new ProductImage()
                {
                    DateCreated = DateTime.Now,
                    ImagePath = await this.SaveFile(file),
                    Caption = $"Thumbnail Image [{sortOrderNew + 1}]",
                    SortOrder = sortOrderNew + 1,
                    IsDefault = true,
                    FileSize = file.Length,
                    ProductId = productId
                });
            }
            await _context.ProductImages.AddRangeAsync(listProductImages);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> UpdateImage(int imageId, string caption, bool isDefault)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) throw new eShopException($"Cannot find imageId:{imageId}");
            image.Caption = caption;
            image.IsDefault = isDefault;
            _context.ProductImages.Update(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<int> DeleteImage(int imageId)
        {
            var image = await _context.ProductImages.FindAsync(imageId);
            if (image == null) throw new eShopException($"Cannot find imageId:{imageId}");
            await _storageService.DeleteFileAsync(image.ImagePath);
            _context.ProductImages.Remove(image);
            return await _context.SaveChangesAsync();
        }

        public async Task<List<ProductImageViewModel>> GetListImages(int productId)
        {
            var listImages = from p in _context.Products
                             join pi in _context.ProductImages on p.Id equals pi.ProductId
                             select new { p, pi };
            if (productId > 0)
            {
                listImages = listImages.Where(x => x.pi.ProductId == productId);
            }
            var data = await listImages
                .Select(x=>new ProductImageViewModel() 
                {
                    Id=x.pi.Id,
                    FilePath=x.pi.ImagePath,
                    FileSize=x.pi.FileSize,
                    IsDefault=x.pi.IsDefault
                }).ToListAsync();
            return data;
        }
    }
}
