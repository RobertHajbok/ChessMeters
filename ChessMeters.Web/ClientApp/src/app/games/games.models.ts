import { Time } from "@angular/common";

export interface Game {
  id: number;
  moves: string;
  result: string;
  event: string;
  white: string;
  black: string;
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
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
