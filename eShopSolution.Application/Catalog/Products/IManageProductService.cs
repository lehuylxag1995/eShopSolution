using eShopSolution.ViewModels.Catalog.Products;
using eShopSolution.ViewModels.Common;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eShopSolution.Application.Catalog.Products
{
    public interface IManageProductService
    {
        //The method process info Product
        Task<int> Create(ProductCreateRequest request);

        Task<int> Update(ProductUpdateRequest request);

        Task<int> Delete(int ProductId);

        Task<bool> UpdatePrice(int productId, decimal newPrice);

        Task<bool> UpdateStock(int productId, int addedQuantity);

        Task AddViewCount(int productId);

        Task<PageResult<ProductViewModel>> GetAllPaging(GetManageProductPagingRequest request);

        //The method process image separate for Product
        Task<int> AddImages(int productId, List<IFormFile> listFile);

        Task<int> UpdateImage(int imageId, string caption, bool isDefault);

        Task<int> DeleteImage(int imageId);

        Task<List<ProductImageViewModel>> GetListImages(int productId);

    }
}
