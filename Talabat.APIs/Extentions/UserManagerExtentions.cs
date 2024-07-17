using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Talabat.Core.Entities.Identity;

namespace Talabat.APIs.Extentions
{
    public static class UserManagerExtentions
    {
        public static async Task<AppUser> FindUserWithNavigationsAsync(this UserManager<AppUser> userManager, ClaimsPrincipal currentUser)
        {
            var Email = currentUser.FindFirstValue(ClaimTypes.Email);

            var User = await userManager.Users.Include(U => U.Address).FirstOrDefaultAsync(U => U.Email == Email);

            return User;
        }
    }
}
