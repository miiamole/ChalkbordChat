using ChackBoard.Data.Model;
using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChalktBoardChat.UI.Pages
{
    public class IndexModel : PageModel
    {
        public UserServices Uservice;
        public MessageServices Mservice { get; set; }
        private IdentityUser user { get; set; }
        private List<MessageModel> messages { get; set; }

        public IndexModel(UserServices service, MessageServices mService)
        {
            Uservice = service;
            Mservice = mService;

        }

        public void OnGet()
        {

        }
    }
}
