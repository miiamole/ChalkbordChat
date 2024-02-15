namespace Chalkboard.App
{
    private UserManager<IdentityUser> _userManager;
    private SignInManager<IdentityUser> _signInManager;
    public class UserManager(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
    {
        _signInManager = signInManager;
        _userManager = userManager;
    }


    public async Task<IActionResult> UserToLogIn(string username)
    {
        return CodePagesEncodingProvider()
    }
}
