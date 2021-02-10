using ChessMeters.Core.Entities;
using System.Collections.Generic;

namespace ChessMeters.Core.Reports
{
    public interface IBoardState
    {
        bool BlackCastledLong { get; }

        bool BlackCastledShort { get; }

        int BlackDevelopedMinorPiecesCount { get; }

        TreeMove CurrentTreeMove { get; }

        bool DidBlackAlreadyDevelopAllMinorPieces { get; }

        bool DidWhiteAlreadyDevelopAllMinorPieces { get; }

        bool IsBlackCastling { get; }

        bool IsWhiteCastling { get; }

        IEnumerable<TreeMove> Moves { get; }

        TreeMove PreviousTreeMove { get; }

        bool WhiteCastledLong { get; }

        bool WhiteCastledShort { get; }

        int WhiteDevelopedMinorPiecesCount { get; }

        void Initialize(IEnumerable<TreeMove> moves);

        void SetNextTreeMove();
    }
}