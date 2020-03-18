using System.IO;
using System.Threading.Tasks;

namespace eShopSolution.Application.Common
{
    public interface IStorageService
    {
        string GetFileUrl(string filename);

        Task SaveFileAsync(Stream mediaBinaryStream, string filename);

        Task DeleteFileAsync(string filename);
    }
}
