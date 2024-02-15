using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{
	public class IndexModel : PageModel
	{

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		public string Username { get; set; }

		public string Password { get; set; }

		public IndexModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}



		public void OnGet()
		{
		}
		//lyckas man logga in- skicka till message sidan
	}
}
