using System;

namespace ChessMeters.Web.ViewModels
{
    public class ReportViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public DateTime CreationDate { get; set; }

        public string PGN { get; set; }
    }
}
