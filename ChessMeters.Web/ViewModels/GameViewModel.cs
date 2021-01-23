namespace ChessMeters.Web.ViewModels
{
    public class GameViewModel
    {
        public int Id { get; set; }

        public string Moves { get; set; }

        public string Result { get; set; }

        // public string Site { get; internal set; }
    
        // public string Round { get; internal set; }
        public string Event { get; set; }
        public string Site { get; set; }
        public string Round { get; set; }
        public string White { get; set; }
        public string Black { get; set; }
        public object WhiteElo { get; set; }
        public short BlackElo { get; set; }
        public string Eco { get; set; }
        public string TimeControl { get; set; }
        public string Termination { get; set; }
    }
}
    
