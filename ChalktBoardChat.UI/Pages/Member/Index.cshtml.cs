using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages.Member
{
	public class IndexModel : PageModel
	{
		public void OnGet()
		{
			//H�mta alla meddelanden fr�n App, som i sin tur h�mtar fr�n databasen.
			//S�tt listan till list-propertyn i denna klass.
			//P� framsidan, loopa igenom listan och visa ut dem.
		}
	}
}
