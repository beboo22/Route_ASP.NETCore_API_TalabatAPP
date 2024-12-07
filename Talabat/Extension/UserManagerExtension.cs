using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Talabat.Core.Entity.Identity;

namespace Talabat.api.Extension
{
    public static class UserManagerExtension
    {

        public async static Task<AppUser?> FindUserAddressByEmailAsync(this UserManager<AppUser> _Usermanager ,string email)
        {
            var user = await _Usermanager.Users
                            .Include(u=>u.Address)
                            .FirstOrDefaultAsync(u=>u.NormalizedEmail.ToLower() == email.ToLower());

            return user??null;
        }
    }
}
