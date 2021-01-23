using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class GameDetailsViewModel
    {
        public IEnumerable<TreeMoveViewModel> TreeMoves { get; set; } = new List<TreeMoveViewModel>();

        public string Event { get; set; }
    public string Site { get; internal set; }
    
        public string Round { get; internal set; }
    }
}
