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

        private readonly MessageServices _mServices;
        private RoleServices _roleServices;
        private UserServices _userServices;
        public SettingsModel(UserServices Uservice, MessageServices Mservice)
        {

            _userServices = Uservice;
            _mServices = Mservice;


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
            string? userName = User.Identity.Name;


            if (string.IsNullOrWhiteSpace(userName))
            {
                UsernameErrorMessage = "Could not find a user with the current username. Please sign out and in and try again!";
            }
            else if (string.IsNullOrWhiteSpace(NewUsername))
            {
                UsernameErrorMessage = "No new username has been entered";
            }
            else
            {
                var updateUsernameResult = await _userServices.UpdateUsernameAsync(userName, NewUsername);
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
            string? userName = User.Identity.Name;
            //Kommunicera med app-lagret, skicka med original username. Behöver försöka logga in med hjälp av "original password". Om det går bra, ska vi i nästa steg sätta nytt lösenord.
            //Nedanstående säkerställer att nytt lösenord och bekräftelse-lösenordet stämmer överens.

            if (ModelState.IsValid)
            {
                IdentityResult updatePasswordResult = await _userServices.UpdatePasswordAsync(userName, OriginalPassword, NewPassword);
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
            string? userName = User.Identity.Name;

            //Kommunicera med app-lagret, skicka med original-username på den inloggade användaren (ej från inputfältet) och be om borttag.
            IdentityResult deleteUserResult = await _userServices.DeleteUserAsync(userName);
            if (deleteUserResult.Succeeded == false)
            {
                DeleteUserErrorMessage = "User could not be deleted. Please try again later";
                return Page();
            }
            else
            {
                await _mServices.UpdateMessageForDeletedUserAsync(userName);

                await _userServices.LogOutUser();

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
