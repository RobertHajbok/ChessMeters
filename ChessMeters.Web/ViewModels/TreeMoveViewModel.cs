using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class TreeMoveViewModel
    {
        public short StockfishEvaluationCentipawns { get; set; }

        public string Move { get; set; }

        public IEnumerable<string> Flags { get; set; }
    }
}
