using Library.Core.Models;

namespace Library.Core.Contracts
{
    public interface ICategoryService
    {
        Task<bool> ExistsById(int categoryId);

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
    }
}
