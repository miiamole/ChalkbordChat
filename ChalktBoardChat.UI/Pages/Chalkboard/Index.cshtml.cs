using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{

	[BindProperties]
	public class IndexModel : PageModel
	{

		private readonly UserServices _userServices;
		public string Username { get; set; }

		public string Password { get; set; }

		public string ErrorMessage { get; set; }

		public IndexModel(UserServices userServices)
		{
			_userServices = userServices;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPost()
		{
			IdentityUser logInUser = await _userServices.LogInUser(Username, Password);
			if (logInUser != null)
			{
				return RedirectToPage("/Chalkboard/Message");

			}
			else
			{
				ErrorMessage = "Username or password is wrong!";
				return Page();
			}
		}

	}
}
