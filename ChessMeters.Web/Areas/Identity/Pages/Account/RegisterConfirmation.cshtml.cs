using ChessMeters.Core.Entities;
using ChessMeters.Web.Email;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Hosting;
using MimeKit;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ChessMeters.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterConfirmationModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IEmailSender emailSender;
        private readonly IWebHostEnvironment webHostEnvironment;

        public RegisterConfirmationModel(UserManager<User> userManager, IEmailSender emailSender, IWebHostEnvironment webHostEnvironment)
        {
            this.userManager = userManager;
            this.emailSender = emailSender;
            this.webHostEnvironment = webHostEnvironment;
        }

        public string Email { get; set; }

        public bool DisplayConfirmAccountLink { get; set; }

        public string EmailConfirmationUrl { get; set; }

        public async Task<IActionResult> OnGetAsync(string email, string returnUrl = null)
        {
            if (email == null)
            {
                return RedirectToPage("/Index");
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return NotFound($"Unable to load user with email '{email}'.");
            }

            Email = email;
            DisplayConfirmAccountLink = webHostEnvironment.IsDevelopment();
            var userId = await userManager.GetUserIdAsync(user);
            var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            EmailConfirmationUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                protocol: Request.Scheme);

            if (!DisplayConfirmAccountLink)
            {
                var message = new EmailMessage(new MailboxAddress("ChessMeters", "no-reply@chessmeters.com"),
                    new List<MailboxAddress> { new MailboxAddress(email) }, "Register confirmation",
                    $"Please confirm your account by clicking this <a href='{EmailConfirmationUrl}' target='_blank'>link</a>.");
                await emailSender.SendEmail(message);
            }

            return Page();
        }
    }
}
