using Chalkboard.App;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ChalktBoardChat.UI.Pages.Chalkboard
{
    [BindProperties]
    public class RegisterModel : PageModel
    {


        private readonly UserServices _userServices;
        public string? Username { get; set; }

        public string? Password { get; set; }
        [EmailAddress]
        public string Email { get; set; }


        public RegisterModel(UserServices userServices)
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
                    IdentityUser user = await _userServices.RegisterUser(Username, Password, Email);
                    if (user != null)
                    {
                        //logga in användaren
                        var signedInUser = await _userServices.LogInUser(Username, Password);
                        if (signedInUser != null)
                        {

                            return RedirectToPage("/Chalkboard/Message");
                        }
                        //return RedirectToPage("/Chalkboard/Message");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Somthing went wrong.");
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "Something went wrong: " + ex.Message);
                }
            }
            return RedirectToPage("/Chalkboard/Index");
            //return Page(); 
            //Känns som att det borde vara return Page(); fattar dock inte varför
        }
    }
}
