using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Web.ViewModels
{
    public class EditReportViewModel
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
