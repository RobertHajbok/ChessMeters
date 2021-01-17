using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Web.ViewModels
{
    public class GenerateReportViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}
