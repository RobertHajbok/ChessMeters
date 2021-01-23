export interface Game {
  id: number;
  moves: string;
  result: string;
  event: string;
  site: string;
  round: string;
}

export interface GameDetails {
  treeMoves: TreeMove[];
  event: string;
  site: string;
  round: string;
}

interface TreeMove {
  stockfishEvaluationCentipawns: number;
  move: string;
}
