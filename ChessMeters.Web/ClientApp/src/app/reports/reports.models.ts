import { Color, Game } from '../games/games.models';

export interface Report {
  id: number;
  description: string;
  creationTime: Date;
  numberOfGames: number;
  analyzedGames: number;
  analyzeErrorGames: number;
}

export interface GenerateReport {
  description: string;
  pgn: string;
  userColors: Color[];
}

export interface EditReport {
  description: string;
}

export interface ReportDetails {
  description: string;
  games: Game[];
}

export interface ReportAnalyzedGame {
  reportId: number;
  analyzedGames: number;
  analyzeErrorGames: number;
}
