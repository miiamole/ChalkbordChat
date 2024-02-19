using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{
	[BindProperties]
	public class RegisterModel : PageModel
	{


		private readonly UserServices _userServices;
		public string? Username { get; set; }

		public string? Password { get; set; }

		public string Email { get; set; }

		public string ErrorMessage { get; set; }


		public RegisterModel(UserServices userServices)
		{

			_userServices = userServices;
		}
		public void OnGet()
		{
		}

		public async Task<IActionResult> OnPostAsync()
		{
			IdentityUser newUser = await _userServices.RegisterUser(Username, Password, Email);
			if (newUser != null)
			{
				return RedirectToPage("/Chalkboard/Message");
			}
			else
			{
				ErrorMessage = "Something went wrong! Did you choose a longer password with specialtecken, siffor and stor bokatav? ";
				return Page();
			}


		}

	}
}
