namespace ChessMeters.Core.Coach
{
    public interface ICoachBoard
    {
        public int GetCurrentPlyNumber();
        public int GetCurrentMoveNumber();
        public string GetCurrentPly();
        public void NextPly();
        public bool HasNextPly();
        public bool isCurrentPlyWhite();
        public bool isCurrentPlyBlack();

        // Castle.
        public bool isWhiteCastling();
        public bool isBlackCastling();
        public bool DidWhiteAlreadyCastle();
        public bool DidBlackAlreadyCastle();

        // Develop pieces.
        public bool DidWhiteAlreadyDevelopAllMinorPieces();
        public bool DidBlackAlreadyDevelopAllMinorPieces();
        public int GetWhiteDevelopedMinorPiecesCount();
        public int GetBlackDevelopedMinorPiecesCount();

    }
}