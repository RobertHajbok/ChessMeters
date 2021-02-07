using System;
using System.Collections.Generic;
using ChessMeters.Core.Entities;

namespace ChessMeters.Core.Coach
{
    public class CoachBoard : ICoachBoard
    {
        private string algebraic;
        private List<string> plys;
        private int currentPlyNumber = 1;
        private bool whiteCastledShort = false;
        private bool whiteCastledLong = false;
        private bool blackCastledShort = false;
        private bool blackCastledLong = false;
        private List<string> whiteDevelopedMinorPieces = new List<string>();
        private List<string> whiteUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };
        private List<string> blackDevelopedMinorPieces = new List<string>();
        private List<string> blackUndevelopedMinorPieces = new List<string> { "Nb", "Bc", "Bf", "Ng" };
        private List<int> openFiles;

        private ICoachBoardEngineEvaluations stockfishCentipawns;

        public CoachBoard(Game game, ICoachBoardEngineEvaluations stockfishCentipawns)
        {
            this.algebraic = game.Moves;
            this.plys = new List<string>(algebraic.Split(" "));
            this.stockfishCentipawns = stockfishCentipawns;
        }

        public ICoachBoardEngineEvaluations GetStockfishCentipawns()
        {
            return this.stockfishCentipawns;
        }

        public int GetCurrentPlyNumber()
        {
            return currentPlyNumber;
        }

        public int GetCurrentMoveNumber()
        {
            //    m1      m2      m3     m4
            // {p1 p2} {p3 p4} {p5 p6} {p7 p8}
            return currentPlyNumber / 2 + currentPlyNumber % 2;
        }

        public string GetCurrentPly()
        {
            // list[0] = ply1
            // list[1] = ply2
            return plys[currentPlyNumber - 1];
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
                var position = GetCurrentPly().Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                whiteDevelopedMinorPieces.Add(piece);
                whiteUndevelopedMinorPieces.Remove(piece);
            }

            if (IsBlackDevelopingMinorPiece())
            {
                var position = GetCurrentPly().Substring(0, 1);
                var piece = GetPieceFromInitialPosition(position);
                blackDevelopedMinorPieces.Add(piece);
                blackUndevelopedMinorPieces.Remove(piece);
            }
        }

        private string GetPieceFromInitialPosition(string position)
        {
            switch (position)
            {
                case "b":
                    return "Nb";
                case "g":
                    return "Ng";
                case "c":
                    return "Bc";
                case "f":
                    return "Bf";
                default:
                    throw new Exception("Must be a minor piece.");
            }
        }

        public bool HasNextPly()
        {
            return currentPlyNumber < plys.Count - 1;
        }
        public bool IsCurrentPlyWhite()
        {
            // p1, p3, p5, ...
            return (currentPlyNumber % 2) == 1;
        }

        public bool IsCurrentPlyBlack()
        {
            // p2, p4, p6, ...
            return (currentPlyNumber % 2) == 0;
        }

        // Castling / White.
        public bool IsWhiteCastling()
        {
            return isWhiteCastlingShort() || isWhiteCastlingLong();
        }
        private bool isWhiteCastlingShort()
        {
            return GetCurrentPly() == "e1g1";
        }

        private bool isWhiteCastlingLong()
        {
            return GetCurrentPly() == "e1c1";
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
            return GetCurrentPly() == "e8g8";
        }

        private bool isBlackCastlingLong()
        {
            return GetCurrentPly() == "e8c8";
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
            return GetCurrentPly().Contains("b1") && whiteUndevelopedMinorPieces.Contains("Nb");
        }

        private bool IsWhiteDevelopingKnightG()
        {
            return GetCurrentPly().Contains("g1") && whiteUndevelopedMinorPieces.Contains("Ng");
        }

        private bool IsWhiteDevelopingBishopC()
        {
            return GetCurrentPly().Contains("c1") && whiteUndevelopedMinorPieces.Contains("Bc");
        }

        private bool IsWhiteDevelopingBishopF()
        {
            return GetCurrentPly().Contains("f1") && whiteUndevelopedMinorPieces.Contains("Bf");
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
            return GetCurrentPly().Contains("b8") && whiteUndevelopedMinorPieces.Contains("Nb");
        }

        private bool IsBlackDevelopingKnightG()
        {
            return GetCurrentPly().Contains("g8") && whiteUndevelopedMinorPieces.Contains("Ng");
        }

        private bool IsBlackDevelopingBishopC()
        {
            return GetCurrentPly().Contains("c8") && whiteUndevelopedMinorPieces.Contains("Bc");
        }

        private bool IsBlackDevelopingBishopF()
        {
            return GetCurrentPly().Contains("f8") && whiteUndevelopedMinorPieces.Contains("Bf");
        }
    }
}