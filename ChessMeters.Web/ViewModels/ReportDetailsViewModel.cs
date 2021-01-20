using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class ReportDetailsViewModel
    {
        public string Description { get; set; }

        public IEnumerable<GameViewModel> Games { get; set; }
    }
}
