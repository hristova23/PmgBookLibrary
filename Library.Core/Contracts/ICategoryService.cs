using Library.Core.Models;

namespace Library.Core.Contracts
{
    public interface ICategoryService
    {
        Task<bool> CategoryExists(int categoryId);

        Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync();
    }
}
