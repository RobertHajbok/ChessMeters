using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class GameDetailsViewModel
    {
        public IEnumerable<TreeMoveViewModel> TreeMoves { get; set; } = new List<TreeMoveViewModel>();
    }
}
