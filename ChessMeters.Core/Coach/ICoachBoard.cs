using ChessMeters.Core.Entities;
using System.Collections.Generic;

namespace ChessMeters.Core.Coach
{
    public interface ICoachBoard
    {
        IEnumerable<TreeMove> Moves { get; }

        TreeMove PreviousTreeMove { get; }

        TreeMove CurrentTreeMove { get; }

        void NextPly();

        // Castle.
        bool IsWhiteCastling();

        bool IsBlackCastling();

        bool DidWhiteAlreadyCastle();

        bool DidBlackAlreadyCastle();

        // Develop pieces.
        bool DidWhiteAlreadyDevelopAllMinorPieces();

        bool DidBlackAlreadyDevelopAllMinorPieces();

        int GetWhiteDevelopedMinorPiecesCount();

        int GetBlackDevelopedMinorPiecesCount();

        void Initialize(IEnumerable<TreeMove> moves);
    }
}