using ChessMeters.Core.Enums;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ChessMeters.Web.ViewModels
{
    public class GenerateReportViewModel
    {
        [Required]
        [MaxLength(100)]
        public string Description { get; set; }

        [Required]
        public string PGN { get; set; }

        public IEnumerable<ColorEnum> UserColors { get; set; }
    }
}
