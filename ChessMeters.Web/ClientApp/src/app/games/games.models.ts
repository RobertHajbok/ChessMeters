export interface Game {
  id: number;
  moves: string;
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
}

export interface GameDetails {
  treeMoves: TreeMove[];
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
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
