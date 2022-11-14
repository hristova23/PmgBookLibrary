using Library.Core.Contracts;
using Library.Core.Models;
using Library.Infrastructure.Data.Common;
using Library.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository repo;

        public CategoryService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<bool> ExistsById(int categoryId)
        {
            return await repo.AllReadonly<Category>()
                .AnyAsync(c => c.Id == categoryId);
        }

        public async Task<IEnumerable<CategoryViewModel>> GetCategoriesAsync()
        {
            return await repo.AllReadonly<Category>()
                .OrderBy(c => c.Name)
                .Select(c => new CategoryViewModel()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();
        }
    }
}
