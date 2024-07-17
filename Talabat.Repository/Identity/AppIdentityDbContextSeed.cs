using Microsoft.AspNetCore.Identity;
using Talabat.Core.Entities.Identity;

namespace Talabat.Repository.Identity
{
    public static class AppIdentityDbContextSeed
    {
        public static async Task SeedUsersAsync(UserManager<AppUser> userManager)
        {
            if (!userManager.Users.Any())
            {
                var User = new AppUser()
                {
                    DisplayName = "Mohamed Kamal",
                    Email = "KAmal@Gmail.com",
                    UserName = "MohamedElsharif",
                    PhoneNumber = "01122334455"
                };

                await userManager.CreateAsync(User, "Pa$$w0rd");
            }

        }
    }
}
