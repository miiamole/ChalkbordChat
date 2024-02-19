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

            //Skapa en user med password
            var createUserResult = await _userManager.CreateAsync(newUser, password);


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


        public async Task LogOutUser()
        {
            await _signInManager.SignOutAsync();

        }


        public async Task<IdentityResult> UpdateUsernameAsync(string currentUsername, string newUsername)
        {
            IdentityUser? userToUpdate = await _userManager.FindByNameAsync(currentUsername);
            if (userToUpdate != null)
            {
                IdentityResult updateUsernameResult = await _userManager.SetUserNameAsync(userToUpdate, newUsername);
                if (updateUsernameResult.Succeeded)
                {
                    await _userManager.UpdateAsync(userToUpdate);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(userToUpdate, true);
                }
                return updateUsernameResult;
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> UpdatePasswordAsync(string username, string currentPassword, string newPassword)
        {
            IdentityUser? userToUpdate = await _userManager.FindByNameAsync(username);
            if (userToUpdate != null)
            {
                IdentityResult changePasswordResult = await _userManager.ChangePasswordAsync(userToUpdate, currentPassword, newPassword);
                if (changePasswordResult.Succeeded)
                {
                    await _userManager.UpdateAsync(userToUpdate);
                }
                return changePasswordResult;
            }
            return IdentityResult.Failed();
        }

        public async Task<IdentityResult> DeleteUserAsync(string username)
        {
            IdentityUser? userToUpdate = await _userManager.FindByNameAsync(username);
            if (userToUpdate != null)
            {
                return await _userManager.DeleteAsync(userToUpdate);
            }
            return IdentityResult.Failed();

        }
    }
}