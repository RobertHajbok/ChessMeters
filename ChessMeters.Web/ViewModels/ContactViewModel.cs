using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Web.ViewModels
{
    public class ContactViewModel
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string Lastname { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        [MaxLength(100)]
        public string Subject { get; set; }

        [Required]
        [MaxLength(2000)]
        public string Message { get; set; }
    }
}
