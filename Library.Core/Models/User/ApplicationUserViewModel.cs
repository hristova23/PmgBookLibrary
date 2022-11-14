using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Core.Models.User
{
    public class ApplicationUserViewModel
    {
        public string Id { get; set; }
        public string Username { get; set; } = null!;
        public string Email { get; set; } = null!;
        public int Credits { get; set; }
    }
}
