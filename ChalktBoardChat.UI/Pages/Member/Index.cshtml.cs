using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Member
{
	public class IndexModel : PageModel
	{
		public void OnGet()
		{
			//Hämta alla meddelanden från App, som i sin tur hämtar från databasen.
			//Sätt listan till list-propertyn i denna klass.
			//På framsidan, loopa igenom listan och visa ut dem.
		}
	}
}
