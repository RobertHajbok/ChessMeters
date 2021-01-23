﻿using System.Collections.Generic;

namespace ChessMeters.Web.ViewModels
{
    public class GameDetailsViewModel
    {
        public IEnumerable<TreeMoveViewModel> TreeMoves { get; set; } = new List<TreeMoveViewModel>();

        public string Event { get; set; }
    public string Site { get; internal set; }
    
        public string Round { get; internal set; }
        public string Result { get; internal set; }
        public string Black { get; internal set; }
        public string White { get; internal set; }
        public object WhiteElo { get; set; }
        public short BlackElo { get; set; }
        public string Eco { get; set; }
        public string TimeControl { get; set; }
        public string Termination { get; set; }
    }
}
