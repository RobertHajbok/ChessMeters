using System;
using System.Collections.Generic;
using System.Linq;
using ChessMeters.Core.Entities;

namespace ChessMeters.Core.Reports
{
    public class BoardState : IBoardState
    {
        private int currentMoveNumber;
        private List<string> whiteDevelopedMinorPieces;
        private List<string> whiteUndevelopedMinorPieces;
        private List<string> blackDevelopedMinorPieces;
        private List<string> blackUndevelopedMinorPieces;

        public bool WhiteCastledShort { get; private set; }

        public bool WhiteCastledLong { get; private set; }

        public bool BlackCastledShort { get; private set; }

        public bool BlackCastledLong { get; private set; }

        public IEnumerable<TreeMove> Moves { get; private set; }

        public TreeMove PreviousTreeMove { get { return currentMoveNumber != 0 ? Moves.ElementAt(currentMoveNumber - 1) : null; } }

        public TreeMove CurrentTreeMove { get { return Moves.ElementAt(currentMoveNumber); } }

        public void Initialize(IEnumerable<TreeMove> moves)
        {
            Moves = moves;
            currentMoveNumber = 0;

            whiteDevelopedMinorPieces = new List<string>();
            whiteUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };
            blackDevelopedMinorPieces = new List<string>();
            blackUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };

            WhiteCastledShort = WhiteCastledLong = BlackCastledShort = BlackCastledLong = false;
        }

        public void SetNextTreeMove()
        {
            UpdateInternalFlagsForCastingInternalFlags();
            UpdateInternalFlagsForDevelopingMinorPieces();
            currentMoveNumber++;
        }

        private void UpdateInternalFlagsForCastingInternalFlags()
        {
            if (IsWhiteCastlingShort)
            {
                WhiteCastledShort = true;
            }
            if (IsWhiteCastlingLong)
            {
                WhiteCastledLong = true;
            }

            if (IsBlackCastlingShort)
            {
                BlackCastledShort = true;
            }
            if (IsBlackCastlingLong)
            {
                BlackCastledLong = true;
            }
        }

        private void UpdateInternalFlagsForDevelopingMinorPieces()
        {
            if (IsWhiteDevelopingMinorPiece)
            {
                var position = CurrentTreeMove.Move.Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                whiteDevelopedMinorPieces.Add(piece);
                whiteUndevelopedMinorPieces.Remove(piece);
            }

            if (IsBlackDevelopingMinorPiece)
            {
                var position = CurrentTreeMove.Move.Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                blackDevelopedMinorPieces.Add(piece);
                blackUndevelopedMinorPieces.Remove(piece);
            }
        }

        private static string GetPieceFromInitialPosition(string position)
        {
            return position switch
            {
                "b" => "Nb",
                "g" => "Ng",
                "c" => "Bc",
                "f" => "Bf",
                _ => throw new Exception("Must be a minor piece."),
            };
        }

        public bool IsWhiteCastling => IsWhiteCastlingShort || IsWhiteCastlingLong;

        private bool IsWhiteCastlingShort => CurrentTreeMove.Move == "e1g1";

        private bool IsWhiteCastlingLong => CurrentTreeMove.Move == "e1c1";

        public bool IsBlackCastling => IsBlackCastlingShort || IsBlackCastlingLong;

        private bool IsBlackCastlingShort => CurrentTreeMove.Move == "e8g8";

        private bool IsBlackCastlingLong => CurrentTreeMove.Move == "e8c8";

        public bool DidWhiteAlreadyDevelopAllMinorPieces => whiteUndevelopedMinorPieces.Count == 0;

        public bool DidBlackAlreadyDevelopAllMinorPieces => blackUndevelopedMinorPieces.Count == 0;

        public int WhiteDevelopedMinorPiecesCount => whiteDevelopedMinorPieces.Count;

        public int BlackDevelopedMinorPiecesCount => blackDevelopedMinorPieces.Count;

        private bool IsWhiteDevelopingMinorPiece => IsWhiteDevelopingKnight || IsWhiteDevelopingBishop;

        private bool IsWhiteDevelopingKnight => IsWhiteDevelopingKnightB || IsWhiteDevelopingKnightG;

        private bool IsWhiteDevelopingBishop => IsWhiteDevelopingBishopC || IsWhiteDevelopingBishopF;

        private bool IsWhiteDevelopingKnightB => CurrentTreeMove.Move.Contains("b1") && whiteUndevelopedMinorPieces.Contains("Nb");

        private bool IsWhiteDevelopingKnightG => CurrentTreeMove.Move.Contains("g1") && whiteUndevelopedMinorPieces.Contains("Ng");

        private bool IsWhiteDevelopingBishopC => CurrentTreeMove.Move.Contains("c1") && whiteUndevelopedMinorPieces.Contains("Bc");

        private bool IsWhiteDevelopingBishopF => CurrentTreeMove.Move.Contains("f1") && whiteUndevelopedMinorPieces.Contains("Bf");

        private bool IsBlackDevelopingMinorPiece => IsBlackDevelopingKnight || IsBlackDevelopingBishop;

        private bool IsBlackDevelopingKnight => IsBlackDevelopingKnightB || IsBlackDevelopingKnightG;

        private bool IsBlackDevelopingBishop => IsBlackDevelopingBishopC || IsBlackDevelopingBishopF;

        private bool IsBlackDevelopingKnightB => CurrentTreeMove.Move.Contains("b8") && whiteUndevelopedMinorPieces.Contains("Nb");

        private bool IsBlackDevelopingKnightG => CurrentTreeMove.Move.Contains("g8") && whiteUndevelopedMinorPieces.Contains("Ng");

        private bool IsBlackDevelopingBishopC => CurrentTreeMove.Move.Contains("c8") && whiteUndevelopedMinorPieces.Contains("Bc");

        private bool IsBlackDevelopingBishopF => CurrentTreeMove.Move.Contains("f8") && whiteUndevelopedMinorPieces.Contains("Bf");
    }
}