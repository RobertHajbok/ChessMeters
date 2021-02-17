using ChessMeters.Web.Email;
using ChessMeters.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChessMeters.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IEmailSender emailSender;

        public ContactController(IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [HttpPost]
        public async Task<IActionResult> Send([FromBody] ContactViewModel contact)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var message = new EmailMessage(new MailboxAddress($"{contact.FirstName} {contact.Lastname}", contact.Email),
                new List<MailboxAddress> { new MailboxAddress("admin@chessmeters.com") }, contact.Subject, contact.Message);
            await emailSender.SendEmail(message);

            return Ok();
        }
    }
}
