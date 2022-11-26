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

        public async Task<string> GetIdByEmailAsync(string email)
        {
            var user = await repo.All<ApplicationUser>().Where(u => u.Email == email).FirstAsync();
            return user.Id;
        }

        public async Task<bool> ExistsById(string userId)
        {
            return await repo.AllReadonly<ApplicationUser>()
                .AnyAsync(u => u.Id == userId);
        }

        public async Task<IEnumerable<ApplicationUserViewModel>> GetUsersAsync()
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
    }
}
