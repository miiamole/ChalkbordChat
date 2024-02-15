using Chalkboard.App;
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

		public IndexModel(UserServices userServices)
		{
			_userServices = userServices;
		}
		public void OnGet()
		{
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (ModelState.IsValid)
			{
				try
				{
					var user = await _userServices.LogInUser(Username, Password);
					if (user != null)
					{

						return RedirectToPage("/Chalkboard/Message");
					}
					else
					{
						ModelState.AddModelError(string.Empty, "Login failed. Please check username and password");
					}
				}
				catch (Exception ex)
				{
					ModelState.AddModelError(string.Empty, "Something went wrong" + ex.Message);
				}
			}


			return Page();
		}

	}
}
