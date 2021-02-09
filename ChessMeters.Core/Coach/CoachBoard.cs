using System;
using System.Collections.Generic;
using System.Linq;
using ChessMeters.Core.Entities;

namespace ChessMeters.Core.Coach
{
    public class CoachBoard : ICoachBoard
    {
        private int currentPlyNumber;
        private bool whiteCastledShort = false;
        private bool whiteCastledLong = false;
        private bool blackCastledShort = false;
        private bool blackCastledLong = false;
        private readonly List<string> whiteDevelopedMinorPieces = new List<string>();
        private readonly List<string> whiteUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };
        private readonly List<string> blackDevelopedMinorPieces = new List<string>();
        private readonly List<string> blackUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };

        public IEnumerable<TreeMove> Moves { get; private set; }

        public TreeMove PreviousTreeMove { get { return Moves.ElementAt(currentPlyNumber - 1); } }

        public TreeMove CurrentTreeMove { get { return Moves.ElementAt(currentPlyNumber); } }

        public void Initialize(IEnumerable<TreeMove> moves)
        {
            Moves = moves;
        }

        public void NextPly()
        {
            UpdateInternalFlagsForCastingInternalFlags();
            UpdateInternalFlagsForDevelopingMinorPieces();
            currentPlyNumber += 1;
        }

        private void UpdateInternalFlagsForCastingInternalFlags()
        {
            if (isWhiteCastlingShort())
            {
                whiteCastledShort = true;
            }
            if (isWhiteCastlingLong())
            {
                whiteCastledLong = true;
            }

            if (isBlackCastlingShort())
            {
                blackCastledShort = true;
            }
            if (isBlackCastlingLong())
            {
                blackCastledLong = true;
            }
        }
        private void UpdateInternalFlagsForDevelopingMinorPieces()
        {
            if (IsWhiteDevelopingMinorPiece())
            {
                var position = CurrentTreeMove.Move.Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                whiteDevelopedMinorPieces.Add(piece);
                whiteUndevelopedMinorPieces.Remove(piece);
            }

            if (IsBlackDevelopingMinorPiece())
            {
                var position = CurrentTreeMove.Move.Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                blackDevelopedMinorPieces.Add(piece);
                blackUndevelopedMinorPieces.Remove(piece);
            }
        }

        private string GetPieceFromInitialPosition(string position)
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


        // Castling / White.
        public bool IsWhiteCastling()
        {
            return isWhiteCastlingShort() || isWhiteCastlingLong();
        }

        private bool isWhiteCastlingShort()
        {
            return CurrentTreeMove.Move == "e1g1";
        }

        private bool isWhiteCastlingLong()
        {
            return CurrentTreeMove.Move == "e1c1";
        }

        public bool DidWhiteAlreadyCastle()
        {
            return DidWhiteAlreadyCastleShort() || DidWhiteAlreadyCastleLong();
        }

        public bool DidWhiteAlreadyCastleShort()
        {
            return whiteCastledShort;
        }

        public bool DidWhiteAlreadyCastleLong()
        {
            return whiteCastledLong;
        }

        // Castling / Black.
        public bool IsBlackCastling()
        {
            return isBlackCastlingShort() || isBlackCastlingLong();
        }

        private bool isBlackCastlingShort()
        {
            return CurrentTreeMove.Move == "e8g8";
        }

        private bool isBlackCastlingLong()
        {
            return CurrentTreeMove.Move == "e8c8";
        }

        public bool DidBlackAlreadyCastle()
        {
            return DidBlackAlreadyCastleShort() || DidBlackAlreadyCastleLong();
        }

        public bool DidBlackAlreadyCastleShort()
        {
            return blackCastledShort;
        }

        public bool DidBlackAlreadyCastleLong()
        {
            return blackCastledLong;
        }

        // Developing pieces.

        public bool DidWhiteAlreadyDevelopAllMinorPieces()
        {
            return whiteUndevelopedMinorPieces.Count == 0;
        }

        public bool DidBlackAlreadyDevelopAllMinorPieces()
        {
            return blackUndevelopedMinorPieces.Count == 0;
        }

        public int GetWhiteDevelopedMinorPiecesCount()
        {
            return whiteDevelopedMinorPieces.Count;
        }

        public int GetBlackDevelopedMinorPiecesCount()
        {
            return blackDevelopedMinorPieces.Count;
        }

        // Developing pieces / White / minor pieces.
        private bool IsWhiteDevelopingMinorPiece()
        {
            return IsWhiteDevelopingKnight() || IsWhiteDevelopingBishop();
        }

        private bool IsWhiteDevelopingKnight()
        {
            return IsWhiteDevelopingKnightB() || IsWhiteDevelopingKnightG();
        }

        private bool IsWhiteDevelopingBishop()
        {
            return IsWhiteDevelopingBishopC() || IsWhiteDevelopingBishopF();
        }

        private bool IsWhiteDevelopingKnightB()
        {
            return CurrentTreeMove.Move.Contains("b1") && whiteUndevelopedMinorPieces.Contains("Nb");
        }

        private bool IsWhiteDevelopingKnightG()
        {
            return CurrentTreeMove.Move.Contains("g1") && whiteUndevelopedMinorPieces.Contains("Ng");
        }

        private bool IsWhiteDevelopingBishopC()
        {
            return CurrentTreeMove.Move.Contains("c1") && whiteUndevelopedMinorPieces.Contains("Bc");
        }

        private bool IsWhiteDevelopingBishopF()
        {
            return CurrentTreeMove.Move.Contains("f1") && whiteUndevelopedMinorPieces.Contains("Bf");
        }

        // Developing pieces / Black / minor pieces.
        private bool IsBlackDevelopingMinorPiece()
        {
            return IsBlackDevelopingKnight() || IsBlackDevelopingBishop();
        }

        private bool IsBlackDevelopingKnight()
        {
            return IsBlackDevelopingKnightB() || IsBlackDevelopingKnightG();
        }

        private bool IsBlackDevelopingBishop()
        {
            return IsBlackDevelopingBishopC() || IsBlackDevelopingBishopF();
        }

        private bool IsBlackDevelopingKnightB()
        {
            return CurrentTreeMove.Move.Contains("b8") && whiteUndevelopedMinorPieces.Contains("Nb");
        }

        private bool IsBlackDevelopingKnightG()
        {
            return CurrentTreeMove.Move.Contains("g8") && whiteUndevelopedMinorPieces.Contains("Ng");
        }

        private bool IsBlackDevelopingBishopC()
        {
            return CurrentTreeMove.Move.Contains("c8") && whiteUndevelopedMinorPieces.Contains("Bc");
        }

        private bool IsBlackDevelopingBishopF()
        {
            return CurrentTreeMove.Move.Contains("f8") && whiteUndevelopedMinorPieces.Contains("Bf");
        }
    }
}