export interface Game {
  id: number;
  moves: string;
  result: string;
  event: string;
  white: string;
  black: string;
  status: string;
}

export interface GameDetails {
  treeMoves: TreeMove[];
  result: string;
  event: string;
  site: string;
  round: string;
  white: string;
  black: string;
  whiteElo: number;
  blackElo: number;
  eco: string;
  timeControl: string;
  termination: string;
  date: Date;
  endTime: Date;
  utcDate: Date;
  utcTime: Date;
  whiteRatingDiff: string;
  blackRatingDiff: string;
  variant: string;
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}

export interface GamePreview {
  moves: string;
  result: string;
  event: string;
  white: string;
  black: string;
  userColor: Color;
}

export enum Color {
  White = 1,
  Black
}
