using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Chalkboard.App
{
    public class RoleServices : IdentityUser
    {
        private UserManager<IdentityUser> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly HttpContext _httpContext;

        public RoleServices(UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager, HttpContext httpContext)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContext = httpContext;
        }


        private async Task<bool> IsUserAdminAsync(IdentityUser user)
        {
            // IdentityUser? loggedInUser = await _userManager.GetUserAsync(HttpContext.User);

            return await _userManager.IsInRoleAsync(user, "Admin");

        }

        public async Task<bool> MakeAdmin(string username)
        {
            bool success = false;
            bool adminRoleExists = await _roleManager.RoleExistsAsync("Admin");

            if (!adminRoleExists)
            {
                IdentityRole adminRole = new()
                {
                    Name = "Admin"
                };


                var createAdminRoleResult = await _roleManager.CreateAsync(adminRole);

                if (createAdminRoleResult.Succeeded)
                {
                    adminRoleExists = true;
                }

            }

            if (adminRoleExists)
            {
                IdentityUser? loggedInUser = await _userManager.GetUserAsync(_httpContext.User);

                var addToAdminRoleResult = await _userManager.AddToRoleAsync(loggedInUser, "Admin");

                if (addToAdminRoleResult.Succeeded)
                {
                    success = true;
                    return success;
                }
            }

            return success;
        }

    }
}
