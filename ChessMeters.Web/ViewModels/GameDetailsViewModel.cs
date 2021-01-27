using System;
using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class GameDetailsViewModel
    {
        public IEnumerable<TreeMoveViewModel> TreeMoves { get; set; } = new List<TreeMoveViewModel>();

        public string Event { get; set; }

        public string Site { get; set; }

        public string Round { get; set; }

        public string Result { get; set; }

        public string Black { get; set; }

        public string White { get; set; }

        public short? WhiteElo { get; set; }

        public short? BlackElo { get; set; }

        public string Eco { get; set; }

        public string TimeControl { get; set; }

        public string Termination { get; set; }

        public DateTime? Date { get; set; }

        public TimeSpan? EndTime { get; set; }

        public DateTime? UTCDate { get; set; }

        public TimeSpan? UTCTime { get; set; }

        public string WhiteRatingDiff { get; set; }

        public string BlackRatingDiff { get; set; }

        public string Variant { get; set; }
    }
}
