using ChackBoard.Data.Repositories;
using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{
	[BindProperties]
	public class SettingsModel : PageModel
	{

		private readonly MessageRepository _mrepo;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		public SettingsModel(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_signInManager = signInManager;
			_userManager = userManager;

		}
		public string? OriginalUsername { get; set; }
		public string? OriginalPassword { get; set; }
		public string NewPassword { get; set; } = string.Empty;

		[Compare(nameof(NewPassword), ErrorMessage = "Passwords doesn't match!")]
		public string? NewConfirmedPassword { get; set; } = string.Empty;
		public string? NewUsername { get; set; } = string.Empty;
		public string? UsernameErrorMessage { get; set; }
		public List<IdentityError>? PasswordErrorMessages { get; set; } = new();
		public string? DeleteUserErrorMessage { get; set; }

		public void OnGet()
		{

		}
		public async Task<IActionResult> OnPostUpdateUsernameAsync()
		{
			string? currentUsername = _userManager.GetUserName(User);
			if (string.IsNullOrWhiteSpace(_userManager.GetUserName(User)))
			{
				UsernameErrorMessage = "Could not find a user with the current username. Please sign out and in and try again!";
			}
			else if (string.IsNullOrWhiteSpace(NewUsername))
			{
				UsernameErrorMessage = "No new username has been entered";
			}
			else
			{
				var updateUsernameResult = await new UserServices(_userManager, _signInManager).UpdateUsernameAsync(currentUsername, NewUsername);
				if (updateUsernameResult.Errors.Any())
				{
					UsernameErrorMessage = updateUsernameResult.Errors.ToList()[0].Description;
				}
				else if (updateUsernameResult.Succeeded == false)
				{
					UsernameErrorMessage = "Could not find a user with the current username. Please sign out and in and try again!";
				}
			}
			ResetInputFields();
			return Page();
		}

		public async Task<IActionResult> OnPostUpdatePasswordAsync()
		{
			//Kommunicera med app-lagret, skicka med original username. Behöver försöka logga in med hjälp av "original password". Om det går bra, ska vi i nästa steg sätta nytt lösenord.
			//Nedanstående säkerställer att nytt lösenord och bekräftelse-lösenordet stämmer överens.

			if (ModelState.IsValid)
			{
				IdentityResult updatePasswordResult = await new UserServices(_userManager, _signInManager).UpdatePasswordAsync(User.Identity.Name, OriginalPassword, NewPassword);
				if (updatePasswordResult.Errors.Any())
				{
					PasswordErrorMessages = updatePasswordResult.Errors.ToList();
				}
				else if (updatePasswordResult.Succeeded == false)
				{
					PasswordErrorMessages.Add(new IdentityError()
					{
						Description = "Could not find a user with the current username. Please sign out and in and try again!"
					});
				}
			}
			return Page();
		}
		public async Task<IActionResult> OnPostDeleteUserAsync()
		{
			//Kommunicera med app-lagret, skicka med original-username på den inloggade användaren (ej från inputfältet) och be om borttag.
			IdentityResult deleteUserResult = await new UserServices(_userManager, _signInManager).DeleteUserAsync(User.Identity.Name);
			if (deleteUserResult.Succeeded == false)
			{
				DeleteUserErrorMessage = "User could not be deleted. Please try again later";
				return Page();
			}
			else
			{
				await _signInManager.SignOutAsync();
				return RedirectToPage("/Chalkboard/Index");
			}

		}

		private void ResetInputFields()
		{
			foreach (var input in ModelState.Values)
			{
				input.AttemptedValue = "";
				input.RawValue = "";
			}
		}
	}
}
