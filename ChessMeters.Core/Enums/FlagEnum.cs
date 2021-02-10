using System.ComponentModel;

namespace ChessMeters.Core.Enums
{
    public enum FlagEnum
    {
        [Description("This flag should be raised when the current move is a blunder in the given positon")]
        Blunder = 1,

        [Description("This flag should be raised when player hasn't castled before a given move")]
        DidNotCastle,

        [Description("This flag should be raised when player didn't develop all his or her pieces in the opening")]
        DidNotDevelopAllPieces,

        [Description("This flag should be raised when no minor piece was developed in the first 5 moves")]
        DidNotDevelopAtLeastOneMinorPieceBeforeMove5
    }
}
