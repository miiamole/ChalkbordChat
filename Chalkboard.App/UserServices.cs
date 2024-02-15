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


        public async Task<IdentityUser> LogInUser(string username, string password)
        {
            IdentityUser? userToLogIn = await _userManager.FindByNameAsync(username);
            if (userToLogIn != null)
            {
                var signInResult = await _signInManager.PasswordSignInAsync(userToLogIn, password, false, false);
                if (signInResult.Succeeded)
                {
                    return userToLogIn;
                }
                else
                {
                    throw new Exception("Password not a match");
                }
            }

            return null;
        }


        public async Task<IdentityUser> RegisterUser(string username, string password, string email)
        {
            IdentityUser newUser = new()
            {
                UserName = username,
                Email = email
            };

            var createUserResult = await _userManager.CreateAsync(newUser);
            if (createUserResult.Succeeded)
            {
                IdentityUser? userToLogIn = await _userManager.FindByNameAsync(username);

                var createSignInResult = await _signInManager.PasswordSignInAsync(userToLogIn, password, false, true);
                if (createSignInResult.Succeeded)
                {
                    return newUser;
                }
                else
                {
                    throw new Exception("");
                }

            }
            else
            {
                return null;
            }


        }

    }




}
