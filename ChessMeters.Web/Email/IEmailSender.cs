using System.Threading.Tasks;

namespace ChessMeters.Web.Email
{
    public interface IEmailSender
    {
        Task SendEmail(EmailMessage message);
    }
}
