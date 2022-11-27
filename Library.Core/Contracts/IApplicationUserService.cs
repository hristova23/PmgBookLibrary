using Library.Core.Models;
using Library.Core.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Contracts
{
    public interface IApplicationUserService
    {
        Task AddCredits(string userId, int creditsCount);

        Task<ApplicationUserViewModel> GetByIdAsync(string id);

        Task<ApplicationUserViewModel> GetByEmailAsync(string email);

        Task<bool> ExistsById(string userId);

        Task<IEnumerable<ApplicationUserViewModel>> GetAllAsync();
    }
}
