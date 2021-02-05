using System;
using System.Collections.Generic;

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

        public CoachBoard(string algebraic)
        {
            this.algebraic = algebraic;
            this.plys = new List<string>(algebraic.Split(" "));
        }

        public int GetCurrentPlyNumber()
        {
            return this.currentPlyNumber;
        }

        public int GetCurrentMoveNumber()
        {
            //    m1      m2      m3     m4
            // {p1 p2} {p3 p4} {p5 p6} {p7 p8}
            return this.currentPlyNumber / 2 + this.currentPlyNumber % 2;
        }

        public string GetCurrentPly()
        {
            // list[0] = ply1
            // list[1] = ply2
            return this.plys[this.currentPlyNumber - 1];
        }
        public void NextPly()
        {
            this.UpdateInternalFlagsForCastingInternalFlags();
            this.UpdateInternalFlagsForDevelopingMinorPieces();
            this.currentPlyNumber += 1;
        }

        private void UpdateInternalFlagsForCastingInternalFlags()
        {
            if (this.isWhiteCastlingShort())
            {
                this.whiteCastledShort = true;
            }
            if (this.isWhiteCastlingLong())
            {
                this.whiteCastledLong = true;
            }

            if (this.isBlackCastlingShort())
            {
                this.blackCastledShort = true;
            }
            if (this.isBlackCastlingLong())
            {
                this.blackCastledLong = true;
            }
        }
        private void UpdateInternalFlagsForDevelopingMinorPieces()
        {
            if (this.IsWhiteDevelopingMinorPiece())
            {
                var position = this.GetCurrentPly().Substring(0, 1);
                var piece = this.GetPieceFromInitialPosition(position);
                this.whiteDevelopedMinorPieces.Add(piece);
                this.whiteUndevelopedMinorPieces.Remove(piece);
            }

            if (this.IsBlackDevelopingMinorPiece())
            {
                var position = this.GetCurrentPly().Substring(0, 1);
                var piece = this.GetPieceFromInitialPosition(position);
                this.blackDevelopedMinorPieces.Add(piece);
                this.blackUndevelopedMinorPieces.Remove(piece);
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
            return this.currentPlyNumber < this.plys.Count - 1;
        }
        public bool isCurrentPlyWhite()
        {
            // p1, p3, p5, ...
            return (this.currentPlyNumber % 2) == 1;
        }

        public bool isCurrentPlyBlack()
        {
            // p2, p4, p6, ...
            return (this.currentPlyNumber % 2) == 0;
        }

        // Castling / White.
        public bool isWhiteCastling()
        {
            return this.isWhiteCastlingShort() || this.isWhiteCastlingLong();
        }
        private bool isWhiteCastlingShort()
        {
            return this.GetCurrentPly() == "e1g1";
        }

        private bool isWhiteCastlingLong()
        {
            return this.GetCurrentPly() == "e1c1";
        }
        public bool DidWhiteAlreadyCastle()
        {
            return this.DidWhiteAlreadyCastleShort() || this.DidWhiteAlreadyCastleLong();
        }

        public bool DidWhiteAlreadyCastleShort()
        {
            return this.whiteCastledShort;
        }

        public bool DidWhiteAlreadyCastleLong()
        {
            return this.whiteCastledLong;
        }

        // Castling / Black.
        public bool isBlackCastling()
        {
            return this.isBlackCastlingShort() || this.isBlackCastlingLong();
        }
        private bool isBlackCastlingShort()
        {
            return this.GetCurrentPly() == "e8g8";
        }

        private bool isBlackCastlingLong()
        {
            return this.GetCurrentPly() == "e8c8";
        }

        public bool DidBlackAlreadyCastle()
        {
            return this.DidBlackAlreadyCastleShort() || this.DidBlackAlreadyCastleLong();
        }

        public bool DidBlackAlreadyCastleShort()
        {
            return this.blackCastledShort;
        }

        public bool DidBlackAlreadyCastleLong()
        {
            return this.blackCastledLong;
        }

        // Developing pieces.

        public bool DidWhiteAlreadyDevelopAllMinorPieces()
        {
            return this.whiteUndevelopedMinorPieces.Count == 0;
        }

        public bool DidBlackAlreadyDevelopAllMinorPieces()
        {
            return this.blackUndevelopedMinorPieces.Count == 0;
        }

        // Developing pieces / White / minor pieces.
        private bool IsWhiteDevelopingMinorPiece()
        {
            return this.IsWhiteDevelopingKnight() || this.IsWhiteDevelopingBishop();
        }
        private bool IsWhiteDevelopingKnight()
        {
            return this.IsWhiteDevelopingKnightB() || this.IsWhiteDevelopingKnightG();
        }
        private bool IsWhiteDevelopingBishop()
        {
            return this.IsWhiteDevelopingBishopC() || this.IsWhiteDevelopingBishopF();
        }
        private bool IsWhiteDevelopingKnightB()
        {
            return this.GetCurrentPly().Contains("b1") && this.whiteUndevelopedMinorPieces.Contains("Nb");
        }
        private bool IsWhiteDevelopingKnightG()
        {
            return this.GetCurrentPly().Contains("g1") && this.whiteUndevelopedMinorPieces.Contains("Ng");
        }
        private bool IsWhiteDevelopingBishopC()
        {
            return this.GetCurrentPly().Contains("c1") && this.whiteUndevelopedMinorPieces.Contains("Bc");
        }
        private bool IsWhiteDevelopingBishopF()
        {
            return this.GetCurrentPly().Contains("f1") && this.whiteUndevelopedMinorPieces.Contains("Bf");
        }

        // Developing pieces / Black / minor pieces.
        private bool IsBlackDevelopingMinorPiece()
        {
            return this.IsBlackDevelopingKnight() || this.IsBlackDevelopingBishop();
        }
        private bool IsBlackDevelopingKnight()
        {
            return this.IsBlackDevelopingKnightB() || this.IsBlackDevelopingKnightG();
        }
        private bool IsBlackDevelopingBishop()
        {
            return this.IsBlackDevelopingBishopC() || this.IsBlackDevelopingBishopF();
        }
        private bool IsBlackDevelopingKnightB()
        {
            return this.GetCurrentPly().Contains("b8") && this.whiteUndevelopedMinorPieces.Contains("Nb");
        }
        private bool IsBlackDevelopingKnightG()
        {
            return this.GetCurrentPly().Contains("g8") && this.whiteUndevelopedMinorPieces.Contains("Ng");
        }
        private bool IsBlackDevelopingBishopC()
        {
            return this.GetCurrentPly().Contains("c8") && this.whiteUndevelopedMinorPieces.Contains("Bc");
        }
        private bool IsBlackDevelopingBishopF()
        {
            return this.GetCurrentPly().Contains("f8") && this.whiteUndevelopedMinorPieces.Contains("Bf");
        }
    }

}