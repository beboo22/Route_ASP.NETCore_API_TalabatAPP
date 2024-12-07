using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entity.Identity;

namespace Talabat.Core.ServiceContract.Authentication
{
    public interface IAuthentication
    {
        public Task<string> CreateTokenAsync(AppUser _user, UserManager<AppUser> _userManger);
    }
}
