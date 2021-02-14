using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ChessMeters.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ChessMeters.Web.Areas.Identity.Pages.Account.Manage
{
    public class LinkedAccountsModel : PageModel
    {
        private readonly UserManager<User> _userManager;

        public LinkedAccountsModel(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public string LichessAccount { get; set; }

        public string ChessComAccount { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [MaxLength(20)]
            [Display(Name = "Lichess username")]
            public string LichessUsername { get; set; }

            [MaxLength(100)]
            [Display(Name = "Chess.com username")]
            public string ChessComUsername { get; set; }
        }

        private void Load(User user)
        {
            Input = new InputModel
            {
                LichessUsername = user.LichessUsername,
                ChessComUsername = user.ChessComUsername
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            Load(user);
            return Page();
        }

        public async Task<IActionResult> OnPostChangeLinkedAccountsAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                Load(user);
                return Page();
            }

            user.LichessUsername = Input.LichessUsername;
            user.ChessComUsername = Input.ChessComUsername;
            await _userManager.UpdateAsync(user);
            StatusMessage = "Linked accounts successfully updated.";
            return RedirectToPage();
        }
    }
}
