using Microsoft.AspNetCore.Identity;

namespace Chalkboard.App
{

    public class UserServices
    {
        private UserManager<IdentityUser> _userManager;
        private SignInManager<IdentityUser> _signInManager;

        public UserServices(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }


        public async Task<IActionResult<IdentityUser>> UserToLogIn(string username, string password)
        {
            IdentityUser? userToLogIn = await _userManager.FindByNameAsync(username);
            if (userToLogIn != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, password, false, false);
                if (signInResult.Succeeded)
                {
                    return userToLogIn;
                }
            }

            throw new ;
        }

    }



}
