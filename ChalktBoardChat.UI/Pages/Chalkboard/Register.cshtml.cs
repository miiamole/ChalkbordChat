using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{
	public class RegisterModel : PageModel
	{

		private readonly SignInManager<IdentityUser> _signInManager;
		private readonly UserManager<IdentityUser> _userManager;
		public string? Username { get; set; }

		public string? Password { get; set; }

		public string Email { get; set; }


		public RegisterModel(SignInManager<IdentityUser> signInManager, UserManager<IdentityUser> userManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;
		}



		public void OnGet()
		{
		}

		//om man skapat en ny user
		//logga in den
		//skicka dem till message sidan
		//lyckas man inte skapa, skicka dem till index




	}
}
