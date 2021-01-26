import { Game } from '../games/games.models';

export interface Report {
  id: number;
  description: string;
  creationTime: Date;
  numberOfGames: number;
}

export interface GenerateReport {
  description: string;
  pgn: string;
  lichessUsername: string;
}

export interface EditReport {
  description: string;
}

export interface ReportDetails {
  description: string;
  games: Game[];
}
