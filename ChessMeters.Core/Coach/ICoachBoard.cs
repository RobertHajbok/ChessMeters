namespace ChessMeters.Core.Coach
{
    public interface ICoachBoard
    {
        public int GetCurrentPlyNumber();

        public int GetCurrentMoveNumber();

        public string GetCurrentPly();

        public void NextPly();

        public bool HasNextPly();

        public bool IsCurrentPlyWhite();

        public bool IsCurrentPlyBlack();

        // Evaluations.
        public ICoachBoardEngineEvaluations GetStockfishCentipawns();

        // Castle.
        public bool IsWhiteCastling();

        public bool IsBlackCastling();

        public bool DidWhiteAlreadyCastle();

        public bool DidBlackAlreadyCastle();

        // Develop pieces.
        public bool DidWhiteAlreadyDevelopAllMinorPieces();

        public bool DidBlackAlreadyDevelopAllMinorPieces();

        public int GetWhiteDevelopedMinorPiecesCount();

        public int GetBlackDevelopedMinorPiecesCount();
    }
}