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
        Task<bool> ExistsById(string userId);

        Task<ApplicationUserViewModel> GetUserByIdAsync(string id);

        Task<string> GetIdByEmailAsync(string email);

        Task<IEnumerable<ApplicationUserViewModel>> GetUsersAsync();
    }
}
