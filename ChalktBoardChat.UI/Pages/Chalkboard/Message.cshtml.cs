using ChackBoard.Data.Repositories;
using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace ChalktBoardChat.UI.Pages.Chalkboard
{
	public class MessageModel : PageModel
	{
		private readonly MessageRepository _mrepo;
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;
		public List<ChackBoard.Data.Model.MessageModel> AllMessages { get; set; }

		[BindProperty]
		public string Message { get; set; }
		[BindProperty]
		public int MessageToDeleteId { get; set; }
		public string ErrorMessage { get; set; }

		public MessageModel(MessageRepository mRepo, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_mrepo = mRepo;
			_signInManager = signInManager;
			_userManager = userManager;

		}
		public async Task OnGet()
		{
			AllMessages = new MessageServices(_mrepo).GetAll().ToList();
		}

		public async Task<IActionResult> OnPost()
		{
			IdentityUser currentUser = await _userManager.GetUserAsync(User);
			if (currentUser != null && string.IsNullOrEmpty(Message) == false)
			{
				ChackBoard.Data.Model.MessageModel newMessage = new()
				{
					Message = Message,
					Date = DateTime.Now,
					Username = currentUser.UserName
				};
				AllMessages = new MessageServices(_mrepo).CreateMessage(newMessage).ToList();
				return Page();
			}
			AllMessages = new MessageServices(_mrepo).GetAll().ToList();
			ErrorMessage = "No message has been entered!";
			return Page();
		}

		public async Task<IActionResult> OnPostDeleteAsync()
		{
			var msgService = new MessageServices(_mrepo);
			ChackBoard.Data.Model.MessageModel? messageToDelete = msgService.GetMessageById(MessageToDeleteId);
			if (messageToDelete != null)
			{
				bool isDeleted = await msgService.DeleteMessage(messageToDelete);
			}
			AllMessages = msgService.GetAll().ToList();
			return Page();
		}
	}
}
