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
			//H�mta info fr�n den inloggade anv�ndaren via signedin manager och s�tt original username-propertyn

		}
		public async Task OnPostUpdateUsernameAsync()
		{
			//Kommunicera med app-lagret, skicka med original username och nytt �nskat username.
		}

		public async Task OnPostUpdatePasswordAsync()
		{
			//Kommunicera med app-lagret, skicka med original username. Beh�ver f�rs�ka logga in med hj�lp av "original password". Om det g�r bra, ska vi i n�sta steg s�tta nytt l�senord.
			//Nedanst�ende s�kerst�ller att nytt l�senord och bekr�ftelse-l�senordet st�mmer �verens.
			if (ModelState.IsValid)
			{

			}
		}
		public async Task OnPostDeleteUserAsync()
		{
			//Kommunicera med app-lagret, skicka med original-username p� den inloggade anv�ndaren (ej fr�n inputf�ltet) och be om borttag.

		}
	}
}
