using System.Collections.Generic;

namespace ChessMeters.Core.Coach
{
    public interface ICoachBoardEngineEvaluations
    {
        public int GetCentipawnsForPlyNumber(int ply_number);
    }
}