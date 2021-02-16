using ChessMeters.Core.Attributes;
using System.ComponentModel;

namespace ChessMeters.Core.Enums
{
    public enum FlagEnum
    {
        [Description("This flag should be raised when the current move is a blunder in the given positon")]
        [EnumDisplay(UI = "Blunder")]
        Blunder = 1,

        [Description("This flag should be raised when player hasn't castled before a given move")]
        [EnumDisplay(UI = "Did not castle before move 10")]
        DidNotCastle,

        [Description("This flag should be raised when player didn't develop all his or her pieces in the opening")]
        [EnumDisplay(UI = "Did not develop all minor pieces before move 12")]
        DidNotDevelopAllPieces,

        [Description("This flag should be raised when no minor piece was developed in the first 5 moves")]
        [EnumDisplay(UI = "Did not develop at least one minor piece before move 5")]
        DidNotDevelopAtLeastOneMinorPieceBeforeMove5,

        [Description("This flag should be raised when the current move is an inaccuracy in the given positon")]
        [EnumDisplay(UI = "Inaccuracy")]
        Inaccuracy,

        [Description("This flag should be raised when the current move is a mistake in the given positon")]
        [EnumDisplay(UI = "Mistake")]
        Mistake
    }
}
