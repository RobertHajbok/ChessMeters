namespace ChessMeters.Core.Dtos
{
    public class ReportGameAnalyzedDto
    {
        public ReportGameAnalyzedDto(int reportId, int analyzedGames, int analyzeErrorGames)
        {
            ReportId = reportId;
            AnalyzedGames = analyzedGames;
            AnalyzeErrorGames = analyzeErrorGames;
        }

        public int ReportId { get; set; }

        public int AnalyzedGames { get; set; }

        public int AnalyzeErrorGames { get; set; }
    }
}
