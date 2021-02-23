namespace ChessMeters.Core.Reports
{
    public static class RuleConsts
    {
        public const short inaccuracyThreshold = 50;
        public const short mistakeThreshold = 100;
        public const short blunderThreshold = 200;
        public const short castlingBeforeMove = 10;
        public const short developAtLeastOneMinorPieceBeforeMove = 5;
        public const short developMinorPiecesBeforeMove = 12;
    }
}
