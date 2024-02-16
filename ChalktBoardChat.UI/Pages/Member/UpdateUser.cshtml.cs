using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalktBoardChat.UI.Pages.Member
{
	[BindProperties]
	public class UpdateUserModel : PageModel
	{
		public string? OriginalUsername { get; set; }
		public string? OriginalPassword { get; set; }
		public string NewPassword { get; set; } = string.Empty;

		[Compare(nameof(NewPassword), ErrorMessage = "Passwords doesn't match!")]
		public string NewConfirmedPassword { get; set; } = string.Empty;
		public string? NewUsername { get; set; }

		public void OnGet()
		{
			//Hämta info från den inloggade användaren via signedin manager och sätt original username-propertyn

		}
		public async Task OnPostUpdateUsernameAsync()
		{
			//Kommunicera med app-lagret, skicka med original username och nytt önskat username.
		}

		public async Task OnPostUpdatePasswordAsync()
		{
			//Kommunicera med app-lagret, skicka med original username. Behöver försöka logga in med hjälp av "original password". Om det går bra, ska vi i nästa steg sätta nytt lösenord.
			//Nedanstående säkerställer att nytt lösenord och bekräftelse-lösenordet stämmer överens.
			if (ModelState.IsValid)
			{

			}
		}
		public async Task OnPostDeleteUserAsync()
		{
			//Kommunicera med app-lagret, skicka med original-username på den inloggade användaren (ej från inputfältet) och be om borttag.

		}
	}
}
