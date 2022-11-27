using Library.Core.Contracts;
using Library.Core.Models.User;
using Library.Infrastructure.Data.Common;
using Library.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Library.Core.Services
{
    public class ApplicationUserService : IApplicationUserService
    {
        private readonly IRepository repo;

        public ApplicationUserService(IRepository _repo)
        {
            repo = _repo;
        }

        public async Task<ApplicationUserViewModel> GetByIdAsync(string id)
        {
            if (await this.ExistsById(id) == false)
            {
                return null;
            }

            ApplicationUser user = await repo.GetByIdAsync<ApplicationUser>(id);

            return new ApplicationUserViewModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Credits = user.Credits
            };
        }

        public async Task<ApplicationUserViewModel?> GetByEmailAsync(string email)
        {
            var user = await repo.All<ApplicationUser>()
                .Where(u => u.Email == email).FirstOrDefaultAsync();

            if (user == null)
            {
                return null;
            }

            return new ApplicationUserViewModel()
            {
                Id = user.Id,
                Username = user.UserName,
                Email = user.Email,
                Credits = user.Credits
            };
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await repo.AllReadonly<ApplicationUser>()
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<ApplicationUserViewModel>> GetAllAsync()
        {
            return await repo.AllReadonly<ApplicationUser>()
                .OrderBy(u => u.UserName)
                .Select(u => new ApplicationUserViewModel()
                {
                    Id = u.Id,
                    Username = u.UserName,
                    Email = u.Email,
                    Credits = u.Credits
                })
                .ToListAsync();
        }

        public async Task AddCredits(string userId, int creditsCount)
        {
            var user = await repo.GetByIdAsync<ApplicationUser>(userId);
            user.Credits += creditsCount;

            await repo.SaveChangesAsync();
        }
    }
}
